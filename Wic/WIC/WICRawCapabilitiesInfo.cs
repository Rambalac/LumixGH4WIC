using System;
using System.Runtime.InteropServices;

namespace WIC
{
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct WICRawCapabilitiesInfo
	{
		public uint cbSize;

		public uint CodecMajorVersion;

		public uint CodecMinorVersion;

		public WICRawCapabilities ExposureCompensationSupport;

		public WICRawCapabilities ContrastSupport;

		public WICRawCapabilities RGBWhitePointSupport;

		public WICRawCapabilities NamedWhitePointSupport;

		public uint NamedWhitePointSupportMask;

		public WICRawCapabilities KelvinWhitePointSupport;

		public WICRawCapabilities GammaSupport;

		public WICRawCapabilities TintSupport;

		public WICRawCapabilities SaturationSupport;

		public WICRawCapabilities SharpnessSupport;

		public WICRawCapabilities NoiseReductionSupport;

		public WICRawCapabilities DestinationColorProfileSupport;

		public WICRawCapabilities ToneCurveSupport;

		public WICRawRotationCapabilities RotationSupport;

		public WICRawCapabilities RenderModeSupport;
	}
}
