using System;

namespace UrlRewriteVisualizer.Internal
{
    internal static class EnumEx
    {
        internal static TEnum Parse<TEnum>(string value) where TEnum : struct
        {
            if (string.IsNullOrEmpty(value))
            {
                return default(TEnum);
            }

            return (TEnum)Enum.Parse(typeof(TEnum), value);
        }
    }
}
