using System.ComponentModel;
using System.Reflection;

namespace Easy.Core.Utils
{
    public static class EnumExtensions
    {
        public static T ParseEnumByDescription<T>(this string description) where T : Enum
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return (T)Enum.ToObject(typeof(T), 0);
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

        public static string GetEnumDescription(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var descriptionAttribute = fieldInfo?.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            return descriptionAttribute?.Description ?? value.ToString();
        }
    }
}
