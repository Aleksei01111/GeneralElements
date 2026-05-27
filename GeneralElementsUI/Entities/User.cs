namespace GeneralElementsUI.Entities;

public class User(List<UserField> fields, User.DefaultRoles role)
{
    public enum DefaultRoles
    {
        NonAuthorized,
        Authorized
    }

    public DefaultRoles Role { get; set; } = role;
    
    public List<UserField> Fields { get; set; } = fields;

    public (bool isValid, string? message) Validate()
    {
        foreach (var field in Fields)
        {
            var validationResult = field.Validate();
            if (!validationResult.isValid)
                return (false, validationResult.message);
        }

        return (true, null);
    }
}

public abstract class UserField
{
    public abstract string Name { get; }
    public abstract string Value { get; set; }
    public virtual bool IsRequired { get; set; }
    public virtual UserField? DependentField { get; set; }
    public abstract FieldType Type { get; }

    public virtual (bool isValid, string? message) Validate()
    {
        if (Value == null || (IsRequired && Value.Length == 0)) return (false, $"Обязательное поле '{Name}' не заполнено");
        if (DependentField != null && DependentField.Value != Value) 
            return (false, $"Связанное поле '{DependentField?.Name}' не совпадает с текущим '{Name}'");
        
        return (true, null);
    }
}

public class UserLoginField : UserField
{
    public override string Name { get; } = "Логин";
    public override string Value { get; set; } = "";
    public override bool IsRequired { get; set; } = true;
    public override FieldType Type { get; } = FieldType.Text;
}

public class UserPasswordField : UserField
{
    public override string Name { get; } = "Пароль";
    public override string Value { get; set; }
    public override bool IsRequired { get; set; } = true;
    public override FieldType Type { get; } = FieldType.Password;
}

public class UserRepeatPasswordField(UserPasswordField passwordField) : UserField
{
    public override string Name { get; } = "Повтор пароля";
    public override string Value { get; set; }
    public override bool IsRequired { get; set; } = true;
    public override UserField? DependentField { get; set; } = passwordField;
    public override FieldType Type { get; } = FieldType.Password;
}

public class UserFullNameField : UserField
{
    public override string Name { get; } = "ФИО";
    public override string Value { get; set; }
    public override bool IsRequired { get; set; } = true;
    public override FieldType Type { get; } = FieldType.Text;
}