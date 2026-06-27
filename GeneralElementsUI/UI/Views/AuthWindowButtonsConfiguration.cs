namespace GeneralElementsUI.UI.Views;

/// <summary>
/// Класс для настройки кнопок на окнах авторизации
/// </summary>
/// <param name="canRegister"></param>
/// <param name="canGuest"></param>
public class AuthWindowButtonsConfiguration(bool canRegister, bool canGuest)
{
    public bool CanRegister { get; set; } = canRegister;
    public bool CanGuest { get; set; } = canGuest;
    
    public AuthWindowButtonsConfiguration() : this(true, true) {}
}