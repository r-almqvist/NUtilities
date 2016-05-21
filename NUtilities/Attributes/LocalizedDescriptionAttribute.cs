namespace NUtilities.Attributes
{
    using System;
    using System.ComponentModel;
    using System.Reflection;

    /// <summary>
    /// This class provides support for localized property and event descriptions.
    /// </summary>
    /// <seealso cref="System.ComponentModel.DescriptionAttribute" />
    public class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        private PropertyInfo nameProperty;
        private Type resourceType;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedDescriptionAttribute" /> class.
        /// </summary>
        /// <param name="displayNameKey">The display name key.</param>
        public LocalizedDescriptionAttribute(string displayNameKey) : base(displayNameKey)
        {
        }

        /// <summary>
        /// Gets or sets the type of the resource.
        /// </summary>
        /// <value>
        /// The type of the resource.
        /// </value>
        public Type ResourceType
        {
            get
            {
                return resourceType;
            }

            set
            {
                resourceType = value;
                nameProperty = resourceType.GetProperty(base.Description, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            }
        }

        /// <summary>
        /// Gets the description stored in this attribute.
        /// </summary>
        public override string Description
        {
            get
            {
                if (nameProperty == null)
                {
                    return base.Description;
                }

                return (string)nameProperty.GetValue(nameProperty.DeclaringType, null);
            }
        }
    }
}