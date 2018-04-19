using System;

namespace UrlRewriteVisualizer.Internal
{
    internal static class EnumHelper
    {
        internal static TEnum ParseOrDefault<TEnum>(string value, TEnum defaultValue) where TEnum : struct
        {
            if (string.IsNullOrEmpty(value))
            {
                return default(TEnum);
            }

            return (TEnum)Enum.Parse(typeof(TEnum), value);
        }
    }
}
