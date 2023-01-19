using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.ObjectModel;
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
       

        BackgroundWorker tracking;

        static readonly BlApi.IBl? bl = BlApi.Factory.Get();

        bool keepWork = false;

      


        public static readonly DependencyProperty listOfOrdersDependency =
                DependencyProperty.Register("lisOftOrders", typeof(ObservableCollection<BO.OrderForList>), typeof(Window), new PropertyMetadata(null));
        public ObservableCollection<BO.OrderForList> lisOftOrders
        {
            get { return (ObservableCollection<BO.OrderForList>)GetValue(listOfOrdersDependency); }
            set { SetValue(listOfOrdersDependency, value); }
        }
        public OrdersSimulator()
        {
            InitializeComponent();
            start.Visibility = Visibility.Visible;
            string s = "";
            bl.Order.GetOrders().ToList().ForEach(delegate (BO.OrderForList o) { s += (o.Status+" "); });
            MessageBox.Show(s);

            lisOftOrders = new(bl?.Order.GetOrders()!.OrderBy(o => o!.ID)!);
            tracking= new BackgroundWorker();
            //tracking.DoWork += Tracking_DoWork;
            tracking!.DoWork += Tracking_DoWork;
              
            tracking.ProgressChanged += Tracking_ProgressChanged;
            tracking.RunWorkerCompleted += Tracking_RunWorkerCompleted;
            MessageBox.Show("initi");
            tracking.WorkerReportsProgress = true;
            tracking.WorkerSupportsCancellation = true;
        }


        private void Tracking_DoWork(object? sender, DoWorkEventArgs e)
        {

            //MessageBox.Show("track start");
            int len = (int)e.Argument!;
            while (keepWork)
            {
                for (int i = 0; i < len; i++)
                {
                    //lisOftOrders = new(bl?.Order.GetOrders()!);
                    if (tracking.CancellationPending == true)
                    {
                        e.Cancel = true;
                        //e.Result = stopwatch.ElapsedMilliseconds; // Unnecessary
                        break;
                    }
                    else
                    {
                        // Perform a time consuming operation and report progress.
                        System.Threading.Thread.Sleep(5000);
                        tracking.ReportProgress(i * 100 / len);
                    }
                }
                keepWork = false;
            }
        }
        private void Tracking_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            int count = 0;
            ////int er = e.ProgressPercentage
            DateTime minOrder = DateTime.MaxValue;
            
            int minOrderID = 0;

            DateTime minShip = DateTime.MaxValue;
            int minShipID = 0;


            //MessageBox.Show("progress func");
            //var listO = from x in bl!.Order.GetOrders().ToList()! let sta=x.Status++ select x;
            List<BO.OrderForList> listO = bl!.Order.GetOrders().ToList()!;
            listO.ForEach(delegate (BO.OrderForList o)
            {
                //count = 0;
                //DateTime dat = (DateTime)bl.Order.OrderInfo(o.ID).OrderDate!;
                //int num = new System.Globalization.CultureInfo("th-TH").DateTimeFormat.Calendar.GetWeekOfYear(dat, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
                if (o.Status == BO.OrderStatus.Ordered && bl.Order.OrderInfo(o.ID).OrderDate < minOrder)
                {
                    // && bl.Order.OrderInfo(o.ID).OrderDate< minOrder 

                    minOrderID = o.ID;

                    //bl.Order.UpdateShip(o.ID);
                    //o.Status = BO.OrderStatus.Shipped;
                    //MessageBox.Show("shipped");

                }
                else if (o.Status == BO.OrderStatus.Shipped && bl.Order.OrderInfo(o.ID).ShipDate < minShip)
                {
                    // && bl.Order.OrderInfo(o.ID).ShipDate < minShip 


                    minShipID= o.ID;
                    //bl.Order.UpdateDelivery(o.ID);
                    //MessageBox.Show("arrive");
                    //o.Status = BO.OrderStatus.Arrived;
                }
                if (o.Status == BO.OrderStatus.Arrived)
                    count++;
                //lisOftOrders = new(bl?.Order.GetOrders()!);



            });



            try
            {
                //MessageBox.Show("shipped + " + minOrderID);
                //MessageBox.Show("arrive + " + minShipID);
                try
                {
                    bl!.Order.UpdateShip(minOrderID);
                }
                catch (BO.DoesntExistExeption ex)
                {

                }
                catch (BO.ContradictoryDataExeption ex)
                {

                }
                bl.Order.UpdateDelivery(minShipID);
                //string s = "";
                //bl.Order.GetOrders().ToList().ForEach(delegate (BO.OrderForList o) { s += (o.Status + " "); });
                lisOftOrders = new(bl?.Order.GetOrders()!.OrderBy(o => o!.ID)!);
            }
            catch (BO.DoesntExistExeption ex)
            {

            }
            catch (BO.ContradictoryDataExeption ex)
            {

            }
            if (count == listO.Count())
            {
                //MessageBox.Show("all arivved");
                keepWork = false;
                tracking.CancelAsync();
                stop.Visibility = Visibility.Collapsed;
                start.Visibility = Visibility.Visible;
                
            }

            int progress = e.ProgressPercentage;
            //resultLabel.Content = (progress + "%");
            //resultProgressBar.Value = progress;
            
        }

        private void Tracking_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {

            MessageBox.Show("stop func");
            if (e.Cancelled == true)
            {
                // e.Result throw System.InvalidOperationException
                //resultLabel.Content = "Canceled!";
            }
            else if (e.Error != null)
            {
                // e.Result throw System.Reflection.TargetInvocationException
                //resultLabel.Content = "Error: " + e.Error.Message; //Exception Message
            }
            else
            {

                MessageBox.Show("finished");
                new PL.Admin.OrdersSimulator().Show();
                /*
                long result = (long)e.Result;
                if (result < 1000)
                    resultLabel.Content = "Done after " + result + " ms.";
                else
                    resultLabel.Content = "Done after " + result / 1000 + " sec.";*/
            }

        }

        private void start_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("start click");
            keepWork = true;
            tracking.RunWorkerAsync(10);
            stop.Visibility = Visibility.Visible;
            start.Visibility = Visibility.Collapsed;
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("stop click");
            keepWork = false;
            tracking.CancelAsync();
            start.Visibility = Visibility.Visible;
            stop.Visibility = Visibility.Collapsed;
        }
    }


}
