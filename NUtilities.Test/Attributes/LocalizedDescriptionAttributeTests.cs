namespace NUtilities.Tests.Attributes
{
    using NUnit.Framework;
    using NUtilities.Attributes;
    using System.Diagnostics;
    using System.Globalization;
    using System.Reflection;
    using System.Threading;

    /// <summary>
    /// This class contains test cases for <see cref="LocalizedDescriptionAttribute" /> class.
    /// </summary>
    /// <seealso cref="LocalizedDescriptionAttribute" />
    [TestFixture]
    public class LocalizedDescriptionAttributeTests
    {
        /// <summary>
        /// Test that the <see cref="LocalizedDescriptionAttribute"/> can be retrieved and that the
        /// description is correct.
        /// </summary>
        [TestCase]
        [LocalizedDescription("ValidDescription", ResourceType = typeof(AttributeResources))]
        public void LocalizedDescriptionAttribute_ForMethod_MatchesResourceString()
        {
            StringAssert.IsMatch(AttributeResources.ValidDescription, GetCallerDescription());
        }

        /// <summary>
        /// Test that the description changes according to the current culture setting. This test
        /// cases uses the <see cref="SetUICultureAttribute" /> to initialize the culture to one that
        /// is not included, thus the default <see cref="AttributeResources" /> will be used.
        /// </summary>
        /// <param name="cultureName">Name of the culture to use during test..</param>
        /// <returns>Returns <c>true</c> if test passed, otherwise <c>false</c>.</returns>
        [TestCase("sv-SE", ExpectedResult = false)]
        [TestCase("mk-MK", ExpectedResult = true)]
        [LocalizedDescription("ValidDescription", ResourceType = typeof(AttributeResources))]
        [SetUICulture("mn-MN")]
        public bool LocalizedDescriptionAttribute_OnCultureChange_MatchesCultureSpecificResource(string cultureName)
        {
            string originalResourceValue = AttributeResources.ValidDescription;

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
            return originalResourceValue.Equals(GetCallerDescription());
        }

        private string GetCallerDescription()
        {
            var callerMethodName = new StackTrace().GetFrame(1).GetMethod().Name;
            var callerLocalizedDescriptionAttribute = (LocalizedDescriptionAttribute)GetType()
                .GetMethod(callerMethodName).GetCustomAttribute(typeof(LocalizedDescriptionAttribute));

            Assert.IsNotNull(callerLocalizedDescriptionAttribute);
            return callerLocalizedDescriptionAttribute.Description;
        }
    }
}