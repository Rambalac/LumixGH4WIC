namespace com.azi.tiff
{
    public class IfdType
    {
        public static IfdType Byte = new IfdType {BytesLength = 1};
        public static IfdType UInt16 = new IfdType {BytesLength = 2};
        public static IfdType UInt32 = new IfdType {BytesLength = 4};
        public static IfdType UInt32Fraction = new IfdType {BytesLength = 8};

        public int BytesLength { get; set; }
    }
}