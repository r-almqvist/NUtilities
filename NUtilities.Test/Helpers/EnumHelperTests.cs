namespace NUtilities.Tests.Helpers
{
    using NUnit.Framework;
    using NUtilities.Attributes;
    using NUtilities.Helpers;
    using System;

    /// <summary>
    /// This class contains test cases for <see cref="EnumHelper"/> class.
    /// </summary>
    /// <seealso cref="EnumHelper"/>
    [TestFixture]
    public class EnumHelperTests
    {
        private const string Description = "DescriptionText";

        private enum TestEnum
        {
            [LocalizedDescription(Description)]
            Value,

            ValueWithoutDescription
        }

        /// <summary>
        /// Test that <see cref="EnumHelper.GetDescriptionFromValue(System.Enum)"/> return the
        /// expected description.
        /// </summary>
        [TestCase]
        public void GetDescriptionFromValue_ForValue_ReturnsDescription()
        {
            StringAssert.IsMatch(Description, EnumHelper.GetDescriptionFromValue(TestEnum.Value));
        }

        /// <summary>
        /// Test that <see cref="EnumHelper.GetDescriptionFromValue(System.Enum)"/> returns a string
        /// representation of the value when there is no description associated with it.
        /// </summary>
        [TestCase]
        public void GetDescriptionFromValue_WhenNoDescription_ReturnsValueToString()
        {
            StringAssert.IsMatch(TestEnum.ValueWithoutDescription.ToString(), EnumHelper.GetDescriptionFromValue(TestEnum.ValueWithoutDescription));
        }

        /// <summary>
        /// Test that <see cref="EnumHelper.GetDescriptionFromValue(System.Enum)"/> return null for
        /// null values.
        /// </summary>
        [TestCase]
        public void GetDescriptionFromValue_WhenValueIsNull_ReturnsNull()
        {
            Assert.IsNull(EnumHelper.GetDescriptionFromValue(null));
        }

        /// <summary>
        /// Test that <see cref="EnumHelper.GetValueFromDescription{T}(string)"/> returns the
        /// expected value.
        /// </summary>
        [TestCase]
        public void GetValueFromDescription_ForDescription_ReturnsValue()
        {
            Assert.AreEqual(TestEnum.Value, EnumHelper.GetValueFromDescription<TestEnum>(Description));
        }

        /// <summary>
        /// Test that <see cref="EnumHelper.GetValueFromDescription{T}(string)"/> throws <see
        /// cref="InvalidOperationException"/> when the specified type is not an <see cref="Enum"/>.
        /// </summary>
        [TestCase]
        public void GetValueFromDescription_WhenNotEnum_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => EnumHelper.GetValueFromDescription<string>(Description));
        }

        /// <summary>
        /// Test that <see cref="EnumHelper.GetValueFromDescription{T}(string)"/> throws <see
        /// cref="InvalidOperationException"/> when the specified type is not an <see cref="Enum"/>.
        /// </summary>
        [TestCase]
        public void GetValueFromDescription_ForInvalidDescription_ReturnsDefaultValue()
        {
            Assert.AreEqual(default(TestEnum), EnumHelper.GetValueFromDescription<TestEnum>("Invalid description"));
        }
    }
}