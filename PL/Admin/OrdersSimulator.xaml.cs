using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Globalization;

namespace PL.Admin
{
    /// <summary>
    /// Interaction logic for OrdersSimulator.xaml
    /// </summary>
    public partial class OrdersSimulator : Window
    {
        BackgroundWorker tracking; // tahalihon

        static readonly BlApi.IBl? bl = BlApi.Factory.Get();

        bool keepWork = false;
        bool arrived = false;


        // OBSERVER
        public static readonly DependencyProperty listOfOrdersDependency =
                DependencyProperty.Register("lisOftOrders", typeof(ObservableCollection<BO.OrderForList>), typeof(Window), new PropertyMetadata(null));
        public ObservableCollection<BO.OrderForList> lisOftOrders
        {
            get { return (ObservableCollection<BO.OrderForList>)GetValue(listOfOrdersDependency); }
            set { SetValue(listOfOrdersDependency, value); }
        }

        public OrdersSimulator() // constructor
        {
            InitializeComponent();
            lisOftOrders = new(bl?.Order.GetOrders()!.OrderBy(o => o!.ID)!); // update list of orders
            
            var x = from h in lisOftOrders
                    where h.Status == BO.OrderStatus.Arrived
                    select h; // to know if there is order that was noe delivered

            if (x.Count() == lisOftOrders.Count()) // all orders delivered
            {
                //can't start simulator
                playDontWork.Visibility = Visibility.Visible; 
                start.Visibility = Visibility.Collapsed;
            }
            else
            {
                //can start simulator
                start.Visibility = Visibility.Visible;
            }
            
            tracking= new BackgroundWorker();
            tracking!.DoWork += Tracking_DoWork;
            tracking.ProgressChanged += Tracking_ProgressChanged;
            //tracking.RunWorkerCompleted += Tracking_RunWorkerCompleted;
            
            // to make progress and cancel
            tracking.WorkerReportsProgress = true;
            tracking.WorkerSupportsCancellation = true;
        }

        /// <summary>
        /// DO WORK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tracking_DoWork(object? sender, DoWorkEventArgs e)
        {

            int len = (int)e.Argument!;

            while (keepWork) // not canceled
            {
                for (int i = 0; i < len; i++)
                {
                    if (tracking.CancellationPending == true)
                    {
                        e.Cancel = true;
                        break;
                    }
                    else
                    {
                        // Perform a time consuming operation and report progress.
                        System.Threading.Thread.Sleep(3000);
                        tracking.ReportProgress(i * 100 / len);
                    }
                }
                keepWork = false;
            }
        } 

        /// <summary>
        /// PROGRESS CHANGED
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tracking_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            int count = 0;

            // to know the first order that was not already shiped
            DateTime minOrder = DateTime.MaxValue; 
            int minOrderID = 0;

            // to know the first order that was not already arrived
            DateTime minShip = DateTime.MaxValue;
            int minShipID = 0;

            List<BO.OrderForList> listO = bl!.Order.GetOrders().ToList()!;
            listO.ForEach(delegate (BO.OrderForList o)
            {
                // if order is ordered and was ordered first
                if (o.Status == BO.OrderStatus.Ordered && bl.Order.OrderInfo(o.ID).OrderDate < minOrder)
                {
                    minOrder = (DateTime)bl.Order.OrderInfo(o.ID).OrderDate!;
                    minOrderID = o.ID; // save id
                }

                // if order is shiped and was shipped first
                else if (o.Status == BO.OrderStatus.Shipped && bl.Order.OrderInfo(o.ID).ShipDate < minShip)
                {
                    minShip = (DateTime)bl.Order.OrderInfo(o.ID).ShipDate!;
                    minShipID = o.ID;
                }

                if (o.Status == BO.OrderStatus.Arrived) // to know how amny orders arrived
                    count++;
            });

            Random s_rand = new(); // for dates
            try
            {
                if (minOrderID != 0 && keepWork) // need to update ship
                {
                    try
                    {
                        //  update ship by the order date - make sence
                        bl!.Order.UpdateShip(minOrderID, bl.Order.OrderInfo(minOrderID).OrderDate + new TimeSpan(s_rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 7L)));
                    }
                    catch (BO.DoesntExistExeption) { }
                    catch (BO.ContradictoryDataExeption) { }
                }
                if (minShipID != 0 && keepWork) // need to deliver 
                {
                    if (count == listO.Count() - 1) // if this is the last order was not delivered
                    {
                        // can't play simulator
                        stop.Visibility = Visibility.Collapsed;
                        playDontWork.Visibility = Visibility.Visible;
                    }
                    // update delivery by the ship date - make sence
                    bl.Order.UpdateDelivery(minShipID, bl.Order.OrderInfo(minShipID).ShipDate + new TimeSpan(s_rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 3L)));

                    lisOftOrders = new(bl?.Order.GetOrders()!.OrderBy(o => o!.ID)!); // update list of products
                }
            }
            catch (BO.DoesntExistExeption) { }
            catch (BO.ContradictoryDataExeption) { }

            if (count == listO.Count()) // all was delivered
            {
                keepWork = false;
                tracking.CancelAsync(); // cancel
                stop.Visibility = Visibility.Collapsed;
                start.Visibility = Visibility.Visible;
            }

            lisOftOrders = new(bl?.Order.GetOrders()!.OrderBy(o => o!.ID)!); // update list of products
            var x = from h in lisOftOrders
                    where h.Status == BO.OrderStatus.Arrived
                    select h;
            if (x.Count() == lisOftOrders.Count() && !arrived) // all was delivered
            {
                playDontWork.Visibility = Visibility.Visible;
                close.Visibility = Visibility.Visible;

                allArrived.Visibility = Visibility.Visible;
                start.Visibility = Visibility.Collapsed;
            }
            else
            {
                start.Visibility = Visibility.Visible;
            }

        }

        /// <summary>
        /// RUN WORKER COMPLETED
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void Tracking_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        //{

        //    if (e.Cancelled == true)
        //    {
        //        // e.Result throw System.InvalidOperationException
        //        //resultLabel.Content = "Canceled!";
        //    }
        //    else if (e.Error != null)
        //    {
        //        // e.Result throw System.Reflection.TargetInvocationException
        //        //resultLabel.Content = "Error: " + e.Error.Message; //Exception Message
        //    }
        //    else // everything was OK
        //    {
        //        allArrived.Visibility = Visibility.Visible;
               
        //    }

        //}

        /// <summary>
        /// START CLICK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void start_Click(object sender, RoutedEventArgs e)
        {
           
            keepWork = true;
            tracking.RunWorkerAsync(bl!.Order.GetOrders().Count()); // start do work
            // can stop
            stop.Visibility = Visibility.Visible;
            start.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// STOP CLICK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stop_Click(object sender, RoutedEventArgs e)
        {
            keepWork = false;
            tracking.CancelAsync(); // cancel
            // can play
            start.Visibility = Visibility.Visible;
            stop.Visibility = Visibility.Collapsed;
        }
        
        /// <summary>
        /// INFORMATION BUTTON
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InfoButton_click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = ((BO.OrderForList?)button.DataContext)!.ID;
            new Orders.OrderInfo(id).ShowDialog(); // open inforamtion window
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            allArrived.Visibility=Visibility.Collapsed;
            close.Visibility = Visibility.Collapsed;
            arrived = true;
        }
    }


}
