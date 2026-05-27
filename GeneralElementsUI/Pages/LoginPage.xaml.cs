using System.Windows.Controls;
using GeneralElementsUI.Entities;

namespace GeneralElementsUI.Pages;

public partial class LoginPage : Page
{
    public List<UserField> LoginFields { get; }
    
    public LoginPage(List<UserField> loginFields)
    {
        LoginFields = loginFields;
        
        InitializeComponent();

        DataContext = this;
    }
}