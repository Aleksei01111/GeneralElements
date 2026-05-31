using System.Windows;
using System.Windows.Controls;

namespace GeneralElementsUI.UserControls;

public partial class MultilineTextWithPlaceholder : UserControl
{
    public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.Register(
            nameof(Placeholder),
            typeof(string),
            typeof(MultilineTextWithPlaceholder));
    
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(MultilineTextWithPlaceholder),
            new FrameworkPropertyMetadata(
                string.Empty, 
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public static readonly DependencyProperty IsReadOnlyProperty =
        DependencyProperty.Register(nameof(IsReadOnly),
            typeof(bool),
            typeof(MultilineTextWithPlaceholder));
    
    public event GeneralElementsInputs.GeneralInputsTextChangedEventHandler OnTextChanged;
    
    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public bool IsReadOnly
    {
        get => (bool)GetValue(IsReadOnlyProperty);
        set => SetValue(IsReadOnlyProperty, value);
    }
    
    
    
    public MultilineTextWithPlaceholder()
    {
        InitializeComponent();
    }

    private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        OnTextChanged(sender, e, Text);
    }
}