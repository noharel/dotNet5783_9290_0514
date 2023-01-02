﻿using System;
using System.Collections.Generic;
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

namespace PL.Orders;

/// <summary>
/// Interaction logic for Order.xaml
/// </summary>
public partial class Order : Window
{
    BlApi.IBl? bl = BlApi.Factory.Get(); // get bl from factory
    public Order(int x = 0)
    {
        InitializeComponent();
        status.Text = bl.Order.TrackingOrder(x).Status.ToString();
        orderId.Text = x.ToString();
        

    }


}