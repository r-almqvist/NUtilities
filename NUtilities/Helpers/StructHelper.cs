namespace NUtilities.Helpers
{
    using Attributes;
    using System;
    using System.Linq;
    using System.Runtime.InteropServices;

    /// <summary>
    /// This class contains methods for manipulation of structures.
    /// </summary>
    public static class StructHelper
    {
        /// <summary>
        /// Convert a byte array to struct.
        /// </summary>
        /// <typeparam name="T">The type of struct to convert to.</typeparam>
        /// <param name="rawData">The raw data bytes.</param>
        /// <returns>A new struct of the specified type.</returns>
        public static T BytesToStruct<T>(byte[] rawData) where T : struct
        {
            T result = default(T);

            HandleEndianAttributes(typeof(T), rawData);

            GCHandle handle = GCHandle.Alloc(rawData, GCHandleType.Pinned);

            try
            {
                IntPtr rawDataPtr = handle.AddrOfPinnedObject();
                result = (T)Marshal.PtrToStructure(rawDataPtr, typeof(T));
            }
            finally
            {
                handle.Free();
            }

            return result;
        }

        /// <summary>
        /// Convert a structure to byte array.
        /// </summary>
        /// <typeparam name="T">The type of struct to convert from.</typeparam>
        /// <param name="data">the struct to convert.</param>
        /// <returns>A new byte array of the specified type.</returns>
        public static byte[] StructToBytes<T>(T data) where T : struct
        {
            byte[] rawData = new byte[Marshal.SizeOf(data)];
            GCHandle handle = GCHandle.Alloc(rawData, GCHandleType.Pinned);
            try
            {
                IntPtr rawDataPtr = handle.AddrOfPinnedObject();
                Marshal.StructureToPtr(data, rawDataPtr, false);
            }
            finally
            {
                handle.Free();
            }

            HandleEndianAttributes(typeof(T), rawData);

            return rawData;
        }

        /// <summary>
        /// Respect the endian during conversion.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="data">The data.</param>
        private static void HandleEndianAttributes(Type type, byte[] data)
        {
            var fields = type.GetFields().Where(f => f.IsDefined(typeof(EndianAttribute), false))
                .Select(f => new
                {
                    Field = f,
                    Attribute = (EndianAttribute)f.GetCustomAttributes(typeof(EndianAttribute), false)[0],
                    Offset = Marshal.OffsetOf(type, f.Name).ToInt32()
                }).ToList();

            foreach (var field in fields)
            {
                if ((field.Attribute.Endianness == Endianness.Big && BitConverter.IsLittleEndian) ||
                    (field.Attribute.Endianness == Endianness.Little && !BitConverter.IsLittleEndian))
                {
                    Array.Reverse(data, field.Offset, Marshal.SizeOf(field.Field.FieldType));
                }
            }
        }
    }
}
