using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using GeneralElementsUI.Entities;
using GeneralElementsUI.UserControls;

namespace GeneralElementsUI.Views;

public partial class AuthorizationWindow : Window
{
    private List<UserField> _registrationFields;
    private List<UserField> _loginFields;
    
    private Action<User> _onAuthEnd;

    private Pages.LoginPage _loginPage;
    private Pages.RegistrationPage _registrationPage;

    private string _registerText = "Зарегистрироваться";
    private string _loginText = "Войти";

    public bool CanAuthGuest { get; }
    
    public AuthorizationWindow(List<UserField> registrationFields, 
        List<UserField> loginFields, 
        Action<User> onAuthEnd, 
        Func<User, bool> checkRegister,
        bool canAuthGuest = false)
    {
        CanAuthGuest = canAuthGuest;
        
        _onAuthEnd = onAuthEnd;
        
        _loginPage = new(loginFields);
        _registrationPage = new(registrationFields, checkRegister, RegisterDone);
        
        _registrationFields = registrationFields;
        _loginFields = loginFields;
        
        InitializeComponent();
        DataContext = this;
        
        MainButton.Content = NavigateForTextAndGetNewText(_loginText);
    }

    private void RegisterDone(User obj)
    {
        _onAuthEnd(new User(_registrationFields, User.DefaultRoles.Authorized));
        Close();
    }

    private string NavigateForTextAndGetNewText(string text)
    {
        if (text == _loginText)
        {
            MainFrame.Navigate(_loginPage);
            return _registerText;
        }
        
        MainFrame.Navigate(_registrationPage);
        return _loginText;
    }

    private void Register_OnClick(object sender, RoutedEventArgs e)
    {
        var senderAsButton = sender as Button;
        senderAsButton.Content = NavigateForTextAndGetNewText(senderAsButton.Content.ToString());
    }

    private void AuthorizationGuest_OnClick(object sender, RoutedEventArgs e)
    {
        _onAuthEnd(new User([], User.DefaultRoles.NonAuthorized));
        Close();
    }
}

public class BooleanToVisibilityConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool b && b)
            return Visibility.Visible;
        return Visibility.Hidden;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}