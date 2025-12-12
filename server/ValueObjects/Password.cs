using KePass.Server.Commons.Definitions;
using KePass.Server.ValueObjects.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KePass.Server.ValueObjects;

public struct Password : IValidation
{
    public required byte[] Hash { get; set; }
    public required byte[] Salt { get; set; }
    public required uint Memory { get; set; }
    public required PasswordAlgorithm Algorithm { get; set; }
    public required uint Iterations { get; set; }
    public required uint Parallelism { get; set; }
    public required double Version { get; set; }

    public bool IsValid()
    {
        return
            Hash.Length > 0 &&
            Salt.Length > 0 &&
            Memory > 0 &&
            Iterations > 0 &&
            Parallelism > 0 &&
            Version >= 0;
    }

    public static ValueConverter<Password, string> GetValueConverter()
    {
        return new ValueConverter<Password, string>(password => password.ToString(), value => Password.Parse(value));
    }

    public override string ToString()
    {
        return
            $"algorithm={Algorithm.ToString().ToLowerInvariant()};" +
            $"version={Version};" +
            $"memory={Memory};" +
            $"iterations={Iterations};" +
            $"parallelism={Parallelism};" +
            $"salt={Convert.ToHexString(Salt).ToLowerInvariant()};" +
            $"hash={Convert.ToHexString(Hash).ToLowerInvariant()};";
    }


    public static bool TryParse(string value, out Password? password)
    {
        try
        {
            password = Parse(value);
            return true;
        }
        catch
        {
            password = null;
            return false;
        }
    }

    public static Password Parse(string value)
    {
        var parts = value.Trim().Split(';');

        bool algorithm = false,
            version = false,
            memory = false,
            iterations = false,
            parallelism = false,
            salt = false,
            hash = false;

        var password = new Password
        {
            Algorithm = default,
            Version = 0,
            Memory = 0,
            Iterations = 0,
            Parallelism = 0,
            Salt = [],
            Hash = [],
        };

        foreach (var part in parts)
        {
            var keys = part.Split('=');
            var keyName = keys[0].Trim();
            var keyValue = keys[1].Trim();

            if (!algorithm && keyName == "algorithm")
            {
                algorithm = true;
                password.Algorithm = Enum.Parse<PasswordAlgorithm>(keyValue, true);
            }

            if (!version && keyName == "version")
            {
                version = true;
                password.Version = uint.Parse(keyValue);
            }

            if (!memory && keyName == "memory")
            {
                memory = true;
                password.Memory = uint.Parse(keyValue);
            }

            if (!iterations && keyName == "iterations")
            {
                iterations = true;
                password.Iterations = uint.Parse(keyValue);
            }

            if (!parallelism && keyName == "parallelism")
            {
                parallelism = true;
                password.Parallelism = uint.Parse(keyValue);
            }

            if (!salt && keyName == "salt")
            {
                salt = true;
                password.Salt = Convert.FromBase64String(keyValue);
            }

            if (!hash && keyName == "hash")
            {
                hash = true;
                password.Hash = Convert.FromBase64String(keyValue);
            }
        }

        if (!algorithm || !version || !memory || !iterations || !parallelism || !salt || !hash)
            throw new Exception($"Invalid format: {value}");

        return password;
    }
}