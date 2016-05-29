namespace NUtilities.Tests.Attributes
{
    using NUnit.Framework;
    using NUtilities.Attributes;
    using System;
    using System.Reflection;

    /// <summary>
    /// This class contains test cases for the <see cref="EndianAttribute"/> class. 
    /// The <see cref="EndianAttribute"/> is useful together with the 
    /// <see cref="NUtilities.Helpers.StructHelper"/> class.
    /// </summary>
    /// <seealso cref="Helpers.StructHelperTests"/>
    [TestFixture]
    public class EndianAttributeTests
    {
        [Endian(Endianness.Big)]
        private uint field;

        /// <summary>
        /// Test that the <see cref="EndianAttribute"/> assigned to a field has the expected value.
        /// </summary>
        [TestCase]
        public void EndianAttribute_ForField_HasExpectedValue()
        {
            field += 0xDEFECA7E;

            Type type = typeof(EndianAttributeTests);
            FieldInfo fieldInfo = type.GetField("field", BindingFlags.Instance | BindingFlags.NonPublic);
            var attribute = (EndianAttribute)fieldInfo.GetCustomAttribute(typeof(EndianAttribute));

            Assert.AreEqual(Endianness.Big, attribute.Endianness);
        }
    }
}
