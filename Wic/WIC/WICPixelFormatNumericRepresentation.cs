using System;

namespace WIC
{
	public enum WICPixelFormatNumericRepresentation
	{
		WICPixelFormatNumericRepresentationUnspecified,
		WICPixelFormatNumericRepresentationIndexed,
		WICPixelFormatNumericRepresentationUnsignedInteger,
		WICPixelFormatNumericRepresentationSignedInteger,
		WICPixelFormatNumericRepresentationFixed,
		WICPixelFormatNumericRepresentationFloat,
		WICPixelFormatNumericRepresentation_FORCE_DWORD = 2147483647
	}
}
