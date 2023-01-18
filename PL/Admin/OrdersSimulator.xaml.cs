using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
       
        bool keepWork = true;
        public ObservableCollection<BO.OrderForList> lisOftOrders
        {
            get { return (ObservableCollection<BO.OrderForList>)GetValue(listOfOrdersDependency); }
            set { SetValue(listOfOrdersDependency, value); }
        }
        public static readonly DependencyProperty listOfOrdersDependency = DependencyProperty.Register("lisOftOrders", typeof(ObservableCollection<BO.OrderForList>), typeof(Window), new PropertyMetadata(null));

        public OrdersSimulator()
        {
            InitializeComponent();
            lisOftOrders = new(bl?.Order.GetOrders()!);

            tracking!.DoWork += Tracking_DoWork;
            tracking.ProgressChanged += Tracking_ProgressChanged;
            tracking.RunWorkerCompleted += Tracking_RunWorkerCompleted;

            tracking.WorkerReportsProgress = true;
            tracking.WorkerSupportsCancellation = true;
        }

        private void Tracking_DoWork(object? sender, DoWorkEventArgs e)
        {
            while(keepWork)
            {

            }
         
        }

        private void Tracking_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Tracking_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {

        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {

        }
    }


}
