using System.ComponentModel;
using System.Reflection;

namespace Easy.Core.Utils
{
    public static class EnumExtensions
    {
        public static T ParseEnumByDescription<T>(this string description) where T : System.Enum
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return (T)System.Enum.ToObject(typeof(T), 0);
            }

            var type = typeof(T);
            foreach (var field in type.GetFields())
            {
                var attribute = field.GetCustomAttribute<DescriptionAttribute>();
                if (attribute != null && attribute.Description.Equals(description, StringComparison.OrdinalIgnoreCase))
                {
                    return (T)field.GetValue(null);
                }
            }

            throw new ArgumentException($"'{description}' is not a valid description for {type.Name}");
        }
    }
}
