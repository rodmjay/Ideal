using System;
using System.Collections.Specialized;
using System.Configuration;

namespace Ideal
{
    public static class ConfigurationExtensions
    {
        public static T GetAndRemove<T>(this NameValueCollection config, string propertyName, bool required) where T : IConvertible
        {
            string value = config.Get(propertyName);
            if (required && string.IsNullOrEmpty(value))
                throw new ConfigurationErrorsException($"Expected attribute {propertyName}, which is missing or empty.");
            config.Remove(propertyName);
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}