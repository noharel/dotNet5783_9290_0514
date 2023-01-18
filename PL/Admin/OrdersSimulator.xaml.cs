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
        static readonly BlApi.IBl? bl = BlApi.Factory.Get();
        BackgroundWorker tracking = new BackgroundWorker();
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
        }
    }
}
