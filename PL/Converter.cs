﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PL;


public class IntToStringConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value.ToString();
    }

    //convert from target property type to source property type
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        //try { int.Parse(value.ToString()); }
        if (value is not "") { return int.Parse(value.ToString()); }

        else return null;
    }
}
public class StatusToVisibiltyForShippedConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (((BO.Order)value).Status == BO.OrderStatus.Ordered) return Visibility.Collapsed;
        return Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    //convert from target property type to source property type

}
public class StatusToVisibiltyForArrivedConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (((BO.Order)value).Status == BO.OrderStatus.Shipped) return Visibility.Visible;
        return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    //convert from target property type to source property type

}



