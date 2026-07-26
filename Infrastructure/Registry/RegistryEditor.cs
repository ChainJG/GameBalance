using GameBalance.Core.Results;
using Microsoft.Win32;
using System.Diagnostics.CodeAnalysis;

namespace GameBalance.Infrastructure.Registry
{
    public static class RegistryEditor
    {

        #region Value Methods
        public static ModuleResult SetValue(RegistryEditInfo info, object value)
        {
            try
            {
                using RegistryKey? baseKey = RegistryKey.OpenBaseKey(info.Hive, RegistryView.Default);

                using RegistryKey key = baseKey.CreateSubKey(info.Path, true) ??
                    throw new InvalidOperationException($"Failed to create or open registry key");

                if (info.Kind is null)
                    key.SetValue(info.Key, value);
                else
                    key.SetValue(info.Key, value, info.Kind.Value);

                return ModuleResult.Successful($"Successfully updated {info.Key}");
            }
            catch (Exception ex)
            {
                return ModuleResult.Failed($"Failed to set {info.Key}", ex);
            }
        }
        public static ModuleResult TryGetValue<T>(RegistryEditInfo info, [NotNullWhen(true)] out T? value)
        {
            value = default;

            try
            {
                using RegistryKey? baseKey = RegistryKey.OpenBaseKey(info.Hive, RegistryView.Default);

                using RegistryKey? key = baseKey.OpenSubKey(info.Path);

                if (key is null)
                    return ModuleResult.Failed($"Key not found {info.Path}");


                object? registryValue = key.GetValue(info.Key);

                if (registryValue is null)
                    return ModuleResult.Failed($"Value not found {info.Key}");

                if (registryValue is T typedValue)
                {
                    value = typedValue;
                    return ModuleResult.Successful($"Successfully retrieved {info.Key}");
                }

                value = (T?)Convert.ChangeType(registryValue, typeof(T));

                return value is not null ?
                    ModuleResult.Successful($"Successfully retrieved {info.Key}") :
                    ModuleResult.Failed($"Failed to convert {info.Key} value");
            }
            catch (Exception ex)
            {
                return ModuleResult.Failed($"Failed retrieving {info.Key} value", ex);
            }
        } 
        #endregion

        #region Exists Methods
        public static bool KeyExists(RegistryEditInfo info)
        {
            using RegistryKey? baseKey = RegistryKey.OpenBaseKey(info.Hive, RegistryView.Default);

            using RegistryKey? key = baseKey.OpenSubKey(info.Path);

            return key is not null;
        }

        public static bool ValueExists(RegistryEditInfo info)
        {
            using RegistryKey? baseKey = RegistryKey.OpenBaseKey(info.Hive, RegistryView.Default);

            using RegistryKey? key = baseKey.OpenSubKey(info.Path);

            return key?.GetValue(info.Key) is not null;
        } 
        #endregion

    }
}
