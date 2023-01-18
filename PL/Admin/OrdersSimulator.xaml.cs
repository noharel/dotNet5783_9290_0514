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
        public ObservableCollection<BO.OrderForList> lisOftOrders
        {
            get { return (ObservableCollection<BO.OrderForList>)GetValue(listOfOrdersDependency); }
            set { SetValue(listOfOrdersDependency, value); }
        }
        public static readonly DependencyProperty listOfOrdersDependency =
            DependencyProperty.Register("lisOftOrders", typeof(ObservableCollection<BO.OrderForList>), typeof(Window), new PropertyMetadata(null));

        public OrdersSimulator()
        {
            InitializeComponent();

           

            lisOftOrders = new(bl?.Order.GetOrders()!);
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

            MessageBox.Show("track start");
            int len = (int)e.Argument!;
            while (keepWork)
            {

                for (int i = 0; i < len; i++)
                {

                    if (tracking.CancellationPending == true)
                    {
                        e.Cancel = true;
                        //e.Result = stopwatch.ElapsedMilliseconds; // Unnecessary
                        break;
                    }
                    else
                    {
                        // Perform a time consuming operation and report progress.
                        System.Threading.Thread.Sleep(500);
                        tracking.ReportProgress(i * 100 / len);
                    }


                }
            }
        }
        private void Tracking_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            List<BO.OrderForList> listO = bl!.Order.GetOrders().ToList()!;
            listO.ForEach(delegate (BO.OrderForList o)
            {
                if(o.Status == BO.OrderStatus.Ordered)
                {
                    o.Status = BO.OrderStatus.Shipped;
                    MessageBox.Show("shipped");

                }
                else if(o.Status == BO.OrderStatus.Shipped)
                {
                    o.Status = BO.OrderStatus.Arrived;
                }
            });


            int progress = e.ProgressPercentage;
            //resultLabel.Content = (progress + "%");
            //resultProgressBar.Value = progress;

        }

        private void Tracking_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
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
            keepWork = true;
            tracking.RunWorkerAsync(50);
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            keepWork = false;
            tracking.CancelAsync();
        }
    }


}
