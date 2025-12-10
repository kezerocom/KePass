using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KePass.Server.Types;

public static class VersionExtension
{
    public static ValueConverter<Version, string> GetValueConverter()
    {
        return new ValueConverter<Version, string>(version => version.ToString(), value => Version.Parse(value));
    }
}