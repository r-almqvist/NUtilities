namespace NUtilities.Tests.Helpers
{
    using NUnit.Framework;
    using NUtilities.Attributes;
    using NUtilities.Helpers;

    /// <summary>
    /// This class contains test cases for the <see cref="StructHelper"/> class. 
    /// The <see cref="StructHelper"/> class is useful together with the <see cref="EndianAttribute"/>.
    /// </summary>
    /// <seealso cref="Attributes.EndianAttributeTests"/>
    [TestFixture]
    public class StructHelperTests
    {
        /// <summary>
        /// Try that conversion of raw data into a structure without explicitly declared 
        /// <see cref="EndianAttribute"/> defaults to using little endian during conversion.
        /// </summary>
        [TestCase]
        public void BytesToStruct_WhenNoExplicitEndianness_DefaultsToLittleEndian()
        {
            var rawData = new byte[] { 0x11, 0x22, 0x33, 0x44 };
            var convertedStruct = StructHelper.BytesToStruct<NormalStruct>(rawData);

            /* 
             * The field has no endian attribute, thus we expect little endian as the system default. 
             * I.e. least significant byte at the lowest address.
             */
            Assert.AreEqual(0x44332211, convertedStruct.Field);
        }

        /// <summary>
        /// Try that conversion of raw data into a structure with mixed <see cref="EndianAttribute"/>
        /// handles a fields individual <see cref="Endianness"/>.
        /// </summary>
        [TestCase]
        public void BytesToStruct_ForStructWithMixedEndian_HandlesIndividualEndianness()
        {
            var rawData = new byte[] { 0x11, 0x22, 0x33, 0x44, 0x11, 0x22, 0x33, 0x44 };
            var convertedStruct = StructHelper.BytesToStruct<StructWithMixedEndian>(rawData);

            Assert.AreEqual(0x44332211, convertedStruct.Field1);
            Assert.AreEqual(0x11223344, convertedStruct.Field2);
        }

        /// <summary>
        /// Try that a structure with no explicit <see cref="EndianAttribute"/> converts into raw
        /// data of little endian order.
        /// </summary>
        /// <exception cref="System.NotImplementedException">Not implemented yet.</exception>
        public void StructsToByte_WhenNoExplicitEndianness_DefaultsToLittleEndian()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Try that conversion of a structure with <see cref="EndianAttribute"/> of mixed 
        /// <see cref="Endianness"/> converts into raw data where the mixed endian order is reflected.
        /// </summary>
        /// <exception cref="System.NotImplementedException">Not implemented yet.</exception>
        public void StructsToByte_ForStructWithMixedEndian_HandlesIndividualEndianness()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Structure with no explicitly declared <see cref="EndianAttribute"/>.
        /// </summary>
        public struct NormalStruct
        {
            public int Field;
        }

        /// <summary>
        /// Structure with <see cref="EndianAttribute"/> of mixed <see cref="Endianness"/>.
        /// </summary>
        public struct StructWithMixedEndian
        {
            [Endian(Endianness.Little)]
            public int Field1;

            [Endian(Endianness.Big)]
            public int Field2;
        }
    }
}
