using System;
using System.Runtime.InteropServices;

namespace WIC
{
	[ComConversionLoss]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct _wireBRECORD
	{
		public uint fFlags;

		public uint clSize;

		[MarshalAs(UnmanagedType.Interface)]
		public IRecordInfo pRecInfo;

		[ComConversionLoss]
		public IntPtr pRecord;
	}
}
