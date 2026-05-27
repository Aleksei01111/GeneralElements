using System.Windows;
using System.Windows.Controls;
using GeneralElementsUI.Entities;

namespace GeneralElementsUI.Pages;

public partial class LoginPage : Page
{
    private User _user;
    private Func<User, bool> _checkLogin;
    private Action<User> _onLoginDone;
    
    public List<UserField> LoginFields { get; }
    
    public LoginPage(List<UserField> loginFields, Func<User, bool> checkLogin, Action<User> onLoginDone)
    {
        _checkLogin = checkLogin;
        LoginFields = loginFields;
        _user = new User(loginFields, User.DefaultRoles.Authorized);
        _onLoginDone = onLoginDone;
        
        InitializeComponent();

        DataContext = this;
    }

    private void Login_OnClick(object sender, RoutedEventArgs e)
    {
        var validationResult = _user.Validate();
        if (!validationResult.isValid)
        {
            MessageBox.Show(validationResult.message);
            return;
        }

        if (_checkLogin(_user))
            _onLoginDone(_user);
    }
}