namespace NUtilities.Helpers
{
    using System;
    using System.ComponentModel;
    using System.Reflection;

    /// <summary>
    /// Class containing helper methods related to the <see cref="Enum"/> type.
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Gets description for the specified value.
        /// </summary>
        /// <param name="value">The <see cref="Enum"/> value.</param>
        /// <returns>The description associated with the value.</returns>
        public static string GetDescriptionFromValue(Enum value)
        {
            if (value == null)
            {
                return null;
            }

            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        /// <summary>
        /// Gets <see cref="Enum"/> value from description.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of the <see cref="Enum"/>.</typeparam>
        /// <param name="description">The description.</param>
        /// <returns>
        /// The value that matches the description.
        /// </returns>
        /// <exception cref="InvalidOperationException">The specified type is not an <see cref="Enum"/>.</exception>
        public static T GetValueFromDescriptio<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum)
            {
                throw new InvalidOperationException();
            }

            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                    {
                            return (T)field.GetValue(null);
                    }
                }
                else
                {
                    if (field.Name == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
            }

            return default(T);
        }
    }
}
