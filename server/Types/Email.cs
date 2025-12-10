using System.Text.RegularExpressions;
using KePass.Server.Types.Definitions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KePass.Server.Types;

public readonly struct Email : IValidation
{
    public string Value { get; private init; }
    public string Domain { get; private init; }
    public string Username { get; private init; }

    public static readonly Email Empty = new(string.Empty);

    public Email(string email)
    {
        if (!string.IsNullOrWhiteSpace(email) && TryParse(email, out var result))
        {
            Value = result!.Value.Value;
            Username = result.Value.Username;
            Domain = result.Value.Domain;
        }
        else
        {
            Value = string.Empty;
            Username = string.Empty;
            Domain = string.Empty;
        }
    }

    public bool IsValid()
    {
        return
            !string.IsNullOrWhiteSpace(Value) &&
            !string.IsNullOrWhiteSpace(Username) &&
            !string.IsNullOrWhiteSpace(Domain);
    }

    public static bool TryParse(string value, out Email? email)
    {
        try
        {
            email = Parse(value);
            return true;
        }
        catch
        {
            email = null;
            return false;
        }
    }

    public static Email Parse(string value)
    {
        var formatted = value.ToLowerInvariant().Trim();

        if (formatted.Length is <= 255 and >= 5) // x@x.x = (5)
        {
            if (Regex.IsMatch(formatted, @"^[a-z0-9]+([._-]?[a-z0-9]+)*@[a-z0-9]+([.-]?[a-z0-9]+)*\.[a-z]{2,}$"))
            {
                var parts = formatted.Split('@');

                var email = new Email()
                {
                    Value = formatted.Trim(),
                    Username = parts[0].Trim(),
                    Domain = parts[1].Trim(),
                };

                return email;
            }
        }

        throw new Exception("Invalid email format");
    }


    public static ValueConverter<Email, string> GetValueConverter()
    {
        return new ValueConverter<Email, string>(email => email.Value, value => new Email(value));
    }

    public override string ToString()
    {
        return $"{Username}@{Domain}";
    }

    public override bool Equals(object? value)
    {
        return value is Email email && email.Value == Value;
    }


    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}