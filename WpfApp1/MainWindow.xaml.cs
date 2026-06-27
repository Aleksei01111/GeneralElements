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
using GeneralElementsUI.Views;

namespace WpfApp1;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        var authWindow = new GeneralElementsUI.Views.AuthorizationWindow(
            GeneralElementsUI.Templates.RegisterFieldsTemplates.GetRegisterFieldsVariant2(),
            GeneralElementsUI.Templates.LoginFieldsTemplates.GetLoginFieldsVariant1(),
            OnAuthEnd,
            CheckRegister,
            CheckLogin,
            new AuthWindowButtonsConfiguration(false, false));
        authWindow.OnAuthCancel += () => Environment.Exit(0);
        authWindow.Show();
        
        InitializeComponent();
    }

    private bool CheckLogin(User arg)
    {
        if (arg.Fields.First(u => u is UserLoginField).Value == "admin")
        {
            MessageBox.Show("Такой логин уже есть");
            return false;
        }

        return true;
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

    private void MultilineTextWithPlaceholder_OnOnTextChanged(object sender, TextChangedEventArgs e, string text)
    {
        MessageBox.Show(text);
    }
}