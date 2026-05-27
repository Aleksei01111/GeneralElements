using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GeneralElementsUI.Entities;

namespace WpfApp1;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private List<UserField> _loginFields =
    [
        new UserLoginField(),
        new UserPasswordField(),
    ];

    private List<UserField> _registrationFields = new();
    
    public MainWindow()
    {
        var userLoginRegister = new UserLoginField();
        var userPasswordRegister = new UserPasswordField();
        var userRepeatRegister = new UserRepeatPasswordField(userPasswordRegister);
        var userFullNameRegister = new UserFullNameField();
        
        _registrationFields.AddRange(userLoginRegister, userPasswordRegister, userRepeatRegister, userFullNameRegister);
        
        var authWindow = new GeneralElementsUI.Views.AuthorizationWindow(_registrationFields, _loginFields, OnAuthEnd, CheckRegister, true);
        authWindow.Show();
        
        InitializeComponent();
    }

    private bool CheckRegister(User arg)
    {
        foreach (var field in arg.Fields)
        {
            if (field is UserPasswordField password && password.Value.Length == 4)
            {
                MessageBox.Show("Пароль слишком короткий");
                return false;
            }
        }

        return true;
    }

    private void OnAuthEnd(User user)
    {
        if (user.Role == User.DefaultRoles.NonAuthorized)
            MessageBox.Show("Гость");
        else
            MessageBox.Show(user.Fields.First().Value);
    }
}