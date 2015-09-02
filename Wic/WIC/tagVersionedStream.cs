using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace WIC
{
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct tagVersionedStream
	{
		public Guid guidVersion;

		[MarshalAs(UnmanagedType.Interface)]
		public IStream pStream;
	}
}
