using System;

namespace WIC
{
	public enum WICRawRotationCapabilities
	{
		WICRawRotationCapabilityNotSupported,
		WICRawRotationCapabilityGetSupported,
		WICRawRotationCapabilityNinetyDegreesSupported,
		WICRawRotationCapabilityFullySupported,
		WICRAWROTATIONCAPABILITIES_FORCE_DWORD = 2147483647
	}
}
