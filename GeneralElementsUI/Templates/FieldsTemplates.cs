using System.Net;
using GeneralElementsUI.Entities;

namespace GeneralElementsUI.Templates;

public class LoginFieldsTemplates
{
    /// <summary>
    /// Логин
    /// Пароль
    /// </summary>
    /// <returns></returns>
    public static List<UserField> GetLoginFieldsVariant1()
    {
        return
        [
            new UserLoginField(),
            new UserPasswordField()
        ];
    }
}

public class RegisterFieldsTemplates
{
    /// <summary>
    /// Логин
    /// Пароль
    /// Повторить пароль (связаны)
    /// </summary>
    /// <returns></returns>
    public static List<UserField> GetRegisterFieldsVariant1()
    {
        var login = new UserLoginField();
        var password = new UserPasswordField();
        var passwordRepeat = new UserRepeatPasswordField(password);

        return [login, password, passwordRepeat];
    }

    /// <summary>
    /// Логин
    /// Пароль
    /// Повтор пароля (связано)
    /// ФИО
    /// </summary>
    /// <returns></returns>
    public static List<UserField> GetRegisterFieldsVariant2()
    {
        var fieldsVar1 = GetRegisterFieldsVariant1();
        var res = new List<UserField>(fieldsVar1) { new UserFullNameField() };
        return res;
    }
}