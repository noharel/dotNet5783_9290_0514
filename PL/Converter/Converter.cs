using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PL.Converter;
public class IntToStringConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value.ToString()!;
    }

    //convert from target property type to source property type
    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        //try { int.Parse(value.ToString()); }
        if (value is not "") { return int.Parse(value.ToString()!); }

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
        return "";
    }

    //convert from target property type to source property type

}
public class StatusToVisibiltyForArrivedConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        BO.Order orderStatusValue = (BO.Order)value;

        if (orderStatusValue.Status == BO.OrderStatus.Shipped) return Visibility.Visible;
        return Visibility.Collapsed;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return "";
    }

    //convert from target property type to source property type

}
public class BoolToVisibilityConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool boolValue = (bool)value;
        if (boolValue)
        {
            return Visibility.Visible; //Visibility.Collapsed;
        }
        else
        {
            return Visibility.Hidden;
        }
    }

    //convert from target property type to source property type
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        Visibility visibilityValue = (Visibility)value;
        if (visibilityValue is Visibility.Visible)
        {
            return true; //Visibility.Collapsed;
        }
        else
        {
            return false;
        }
    }
}

public class StatusToPicPathForShippedConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        BO.OrderStatus stat = (BO.OrderStatus)value;

        if (stat == BO.OrderStatus.Ordered) return "..\\pics\\box.png".ToString();
        else if (stat == BO.OrderStatus.Shipped) return "..\\pics\\delivery-truck.webp".ToString();
        else if (stat == BO.OrderStatus.Arrived) return "..\\pics\\house.webp".ToString();
        else return "ERRORR";//invalid status
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return "";
    }

    //convert from target property type to source property type

}


