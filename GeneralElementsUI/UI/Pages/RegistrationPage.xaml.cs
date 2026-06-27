using System.Windows;
using System.Windows.Controls;
using GeneralElementsUI.Entities;

namespace GeneralElementsUI.UI.Pages;

public partial class RegistrationPage : Page
{
    private Func<User, bool> _checkRegister;
    private Action<User> _registerDone;
    private User _user;
    
    public List<UserField>? RegisterFields { get; }
    
    public RegistrationPage(List<UserField>? registerFields, Func<User, bool> checkRegister, Action<User> registerDone)
    {
        if (registerFields == null)
            return;
        
        _checkRegister = checkRegister;
        _registerDone = registerDone;
        
        RegisterFields = registerFields;

        _user = new User(registerFields, User.DefaultRoles.Authorized);
        
        InitializeComponent();

        DataContext = this;
    }

    private void RegisterDone_OnClick(object sender, RoutedEventArgs e)
    {
        if (_user == null)
        {
            if (_checkRegister(_user))
                _registerDone(_user);
            return;
        }
        
        var validResult = _user.Validate();
        if (!validResult.isValid)
        {
            MessageBox.Show(validResult.message);
            return;
        }
        
        if (_checkRegister(_user))
            _registerDone(_user);
    }
}