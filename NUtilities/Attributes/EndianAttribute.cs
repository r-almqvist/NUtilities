namespace NUtilities.Attributes
{
    using System;

    /// <summary>
    /// Defines the endianness.
    /// </summary>
    public enum Endianness
    {
        /// <summary>
        /// The most significant byte is at the lowest address, the other bytes follow in decreasing
        /// order of significance.
        /// </summary>
        Big,

        /// <summary>
        /// The least significant byte is at the lowest address, the other bytes follow in increasing
        /// order of significance.
        /// </summary>
        Little
    }

    /// <summary>
    /// This class provides support for declaring the endian of a field. Use together with <see
    /// cref="NUtilities.Helpers.StructHelper"/> to provide support for structs with field of
    /// different endian order.
    /// </summary>
    /// <seealso cref="System.Attribute"/>
    [AttributeUsage(AttributeTargets.Field)]
    public class EndianAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EndianAttribute"/> class.
        /// </summary>
        /// <param name="endianness">The endian associated with the field.</param>
        public EndianAttribute(Endianness endianness)
        {
            this.Endianness = endianness;
        }

        /// <summary>
        /// Gets the endianness.
        /// </summary>
        /// <value>The endianness.</value>
        public Endianness Endianness { get; private set; }
    }
}
