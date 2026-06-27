using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using GeneralElementsUI.Entities;
using GeneralElementsUI.UserControls;

namespace GeneralElementsUI.Views;

public partial class AuthorizationWindow : Window
{
    /// <summary>
    /// Вызывается при закрытии окно до окончания авторизации
    /// </summary>
    public delegate void OnAuthCancelSignature();

    private bool _authComplete = false;
    
    private List<UserField> _registrationFields;
    private List<UserField> _loginFields;
    
    private Action<User> _onAuthEnd;

    private Pages.LoginPage _loginPage;
    private Pages.RegistrationPage _registrationPage;

    private string _registerText = "Зарегистрироваться";
    private string _loginText = "Войти";

    public AuthWindowButtonsConfiguration ButtonsConfiguration { get; } = new();

    /// <summary>
    /// Вызывается при закрытии окно до окончания авторизации
    /// </summary>
    public OnAuthCancelSignature OnAuthCancel { get; set; }
    
    public AuthorizationWindow(List<UserField> registrationFields, 
        List<UserField> loginFields, 
        Action<User> onAuthEnd, 
        Func<User, bool> checkRegister,
        Func<User, bool> checkLogin,
        bool canGuest = false)
    {
        ButtonsConfiguration.CanGuest = canGuest;
        
        _onAuthEnd = onAuthEnd;
        
        _loginPage = new(loginFields, checkLogin, OnLoginDone);
        _registrationPage = new(registrationFields, checkRegister, RegisterDone);
        
        _registrationFields = registrationFields;
        _loginFields = loginFields;
        
        InitializeComponent();
        DataContext = this;
        
        MainButton.Content = NavigateForTextAndGetNewText(_loginText);
    }
    
    public AuthorizationWindow(List<UserField> registrationFields, 
        List<UserField> loginFields, 
        Action<User> onAuthEnd, 
        Func<User, bool> checkRegister,
        Func<User, bool> checkLogin,
        AuthWindowButtonsConfiguration buttonsConfiguration)
    {
        ButtonsConfiguration = buttonsConfiguration;
        
        _onAuthEnd = onAuthEnd;
        
        _loginPage = new(loginFields, checkLogin, OnLoginDone);
        _registrationPage = new(registrationFields, checkRegister, RegisterDone);
        
        _registrationFields = registrationFields;
        _loginFields = loginFields;
        
        InitializeComponent();
        DataContext = this;
        
        MainButton.Content = NavigateForTextAndGetNewText(_loginText);
    }

    private void OnLoginDone(User obj)
    {
        _onAuthEnd(new User(_loginFields, User.DefaultRoles.Authorized));
        _authComplete = true;
        Close();
    }


    private void RegisterDone(User obj)
    {
        _onAuthEnd(new User(_registrationFields, User.DefaultRoles.Authorized));
        _authComplete = true;
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
        _authComplete = true;
        Close();
    }

    private void AuthorizationWindow_OnClosing(object? sender, CancelEventArgs e)
    {
        if (!_authComplete)
            OnAuthCancel();
    }
}

public class AuthWindowButtonsConfiguration(bool canRegister, bool canGuest)
{
    public bool CanRegister { get; set; } = canRegister;
    public bool CanGuest { get; set; } = canGuest;
    
    public AuthWindowButtonsConfiguration() : this(true, true) {}
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