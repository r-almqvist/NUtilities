namespace NUtilities.Tests.Helpers
{
    using NUnit.Framework;
    using NUtilities.Attributes;
    using NUtilities.Helpers;

    /// <summary>
    /// This class contains test cases for <see cref="EnumHelper" /> class.
    /// </summary>
    /// <seealso cref="EnumHelper" />
    [TestFixture]
    public class EnumHelperTests
    {
        private const string Description = "Description";

        private enum TestEnum
        {
            [LocalizedDescription(Description)]
            Value
        }

        /// <summary>
        /// Test that <see cref="EnumHelper.GetDescriptionFromValue(System.Enum)"/> return the expected description.
        /// </summary>
        [TestCase]
        public void GetDescriptionFromValue_ReturnsExpectedDescription()
        {
            StringAssert.IsMatch(EnumHelper.GetDescriptionFromValue(TestEnum.Value), Description);
        }

        /// <summary>
        /// Test that <see cref="EnumHelper.GetValueFromDescriptio{T}(string)"/> returns the expected value.
        /// </summary>
        [TestCase]
        public void GetValueFromDescription_ReturnsExpectedValue()
        {
            var test = TestEnum.Value.Equals(EnumHelper.GetValueFromDescriptio<TestEnum>(Description));
        }
    }
}
