using System;

namespace WIC
{
	public enum WICMetadataCreationOptions
	{
		WICMetadataCreationDefault,
		WICMetadataCreationAllowUnknown = 0,
		WICMetadataCreationFailUnknown = 65536,
		WICMetadataCreationMask = -65536
	}
}
