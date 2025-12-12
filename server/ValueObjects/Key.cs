using KePass.Server.Commons.Definitions;
using KePass.Server.ValueObjects.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KePass.Server.ValueObjects;

public struct Key : IValidation
{
    public required KeyAlgorithm Algorithm { get; set; }
    public required byte[] PublicKey { get; set; }
    public required byte[] PrivateKey { get; set; }
    public required bool IsEncrypted { get; set; }

    public bool IsValid()
    {
        return PublicKey.Length > 0 && PrivateKey.Length > 0;
    }

    public static ValueConverter<Key, string> GetValueConverter()
    {
        return new ValueConverter<Key, string>(key => key.ToString(), value => Key.Parse(value));
    }

    public override string ToString()
    {
        // algorithm = Algorithm
        // public = PublicKey
        // private = PrivateKey
        // encrypted = Encrypted

        return
            $"algorithm={Algorithm.ToString().ToLowerInvariant()};" +
            $"public={Convert.ToBase64String(PublicKey).ToLowerInvariant()};" +
            $"private={Convert.ToBase64String(PrivateKey).ToLowerInvariant()};" +
            $"encrypted={IsEncrypted.ToString().ToLowerInvariant()}";
    }

    public static bool TryParse(string value, out Key? key)
    {
        try
        {
            key = Parse(value);
            return true;
        }
        catch
        {
            key = null;
            return false;
        }
    }

    public static Key Parse(string value)
    {
        var parts = value.Trim().Split(';');

        bool algorithm = false,
            @public = false,
            @private = false,
            encrypted = false;

        var key = new Key
        {
            Algorithm = default,
            PublicKey = [],
            PrivateKey = [],
            IsEncrypted = false,
        };

        foreach (var part in parts)
        {
            var keys = part.Split('=');
            var keyName = keys[0].Trim();
            var keyValue = keys[1].Trim();

            if (!algorithm && keyName == "algorithm")
            {
                algorithm = true;
                key.Algorithm = Enum.Parse<KeyAlgorithm>(keyValue, true);
            }

            if (!@public && keyName == "public")
            {
                @public = true;
                key.PublicKey = Convert.FromBase64String(keyValue);
            }

            if (!@private && keyName == "private")
            {
                @private = true;
                key.PrivateKey = Convert.FromBase64String(keyValue);
            }

            if (!encrypted && keyName == "encrypted")
            {
                encrypted = true;
                key.IsEncrypted = bool.Parse(keyValue);
            }
        }

        if (!algorithm || !@public || !@private || !encrypted)
            throw new Exception($"Invalid format: {value}");

        return key;
    }
}