using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using GeneralElementsUI.Entities;

namespace GeneralElementsUI.UserControls;

public partial class UniversalTextBoxWithPlaceholder : UserControl
{
    public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(
        nameof(Placeholder), typeof(string), typeof(UniversalTextBoxWithPlaceholder), new PropertyMetadata(default(string)));

    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text), typeof(string), typeof(UniversalTextBoxWithPlaceholder), new PropertyMetadata(default(string)));

    public static readonly DependencyProperty FieldModeProperty = DependencyProperty.Register(
        nameof(FieldMode), typeof(FieldType), typeof(UniversalTextBoxWithPlaceholder),
        new PropertyMetadata(default(FieldType)));

    private FieldType _fieldMode = FieldType.Text;

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    
    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public FieldType FieldMode
    {
        get => (FieldType)GetValue(FieldModeProperty);
        set => SetValue(FieldModeProperty, value);
    }

    public UniversalTextBoxWithPlaceholder()
    {
        InitializeComponent();
    }

    private void PasswordPB_OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        Text = ((PasswordBox)sender).Password;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void ShowPassword_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        PasswordText.Visibility = Visibility.Visible;
    }

    private void HidePassword_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        PasswordText.Visibility = Visibility.Hidden;
    }
}

public class VisibleIfTextBoxConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var valueAsMode = (FieldType)value;
        if(valueAsMode == FieldType.Text)
            return Visibility.Visible;
        return Visibility.Hidden;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class HiddenIfTextBoxConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var valueAsMode = (FieldType)value;
        if(valueAsMode == FieldType.Text)
            return Visibility.Hidden;
        return Visibility.Visible;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}