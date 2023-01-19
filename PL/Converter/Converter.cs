using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

//converters for PL
namespace PL.Converter;

/// <summary>
/// int to string converter
/// </summary>
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
/// <summary>
/// status to visibilty for shipped label
/// </summary>
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
/// <summary>
/// status to visibilty for arrived label
/// </summary>
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
/// <summary>
/// convert bool to visibilty
/// </summary>
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
/// <summary> 
/// convert status to corresponding pic path
/// pic of box-ordered
/// pic of delivery truck-shipped
/// pic of house-delivered
/// </summary>
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
/// <summary>
/// convert status to amount thats should be filled in progress bar
/// 10-ordered
/// 50-shipped
/// 100-delivered
/// </summary>
public class StatusToProgressBarConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        BO.OrderStatus stat = (BO.OrderStatus)value;

        if (stat == BO.OrderStatus.Ordered) return "10";
        else if (stat == BO.OrderStatus.Shipped) return "50";
        else if (stat == BO.OrderStatus.Arrived) return "100";
        else return "ERRORR";//invalid status
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return "";
    }

    //convert from target property type to source property type

}
/// <summary>
/// convert status to matching color
/// red-ordered
/// yellow-shipped
/// green-delivered
/// </summary>
public class StatusToColorrConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        BO.OrderStatus stat = (BO.OrderStatus)value;

        if (stat == BO.OrderStatus.Ordered) return "Red";
        else if (stat == BO.OrderStatus.Shipped) return "Yellow";
        else if (stat == BO.OrderStatus.Arrived) return "Green";
        else return "ERRORR";//invalid status
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return "";
    }

    //convert from target property type to source property type

}
/// <summary>
/// convert a given id of product to visibilty for sold out
/// </summary>
public class IDToSoldOutVisibiltyConverter : IValueConverter
{
    BlApi.IBl? bl = BlApi.Factory.Get(); // get bl from factory

    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        int id = (int)value;
        BO.Product prod = bl!.Product.GetProductInfo_manager(id);

        if (prod.InStock > 0) return Visibility.Collapsed;
        else return Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return "";
    }

    //convert from target property type to source property type

}

