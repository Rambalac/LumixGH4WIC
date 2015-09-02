using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[Guid("0000002F-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IRecordInfo
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void RecordInit([Out] IntPtr pvNew);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void RecordClear([In] IntPtr pvExisting);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void RecordCopy([In] IntPtr pvExisting, [Out] IntPtr pvNew);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetGuid(out Guid pguid);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetName([MarshalAs(UnmanagedType.BStr)] out string pbstrName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetSize(out uint pcbSize);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetTypeInfo([MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "System.Runtime.InteropServices.CustomMarshalers.TypeToTypeInfoMarshaler")] out Type ppTypeInfo);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetField([In] IntPtr pvData, [MarshalAs(UnmanagedType.LPWStr)] [In] string szFieldName, [MarshalAs(UnmanagedType.Struct)] out object pvarField);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetFieldNoCopy([In] IntPtr pvData, [MarshalAs(UnmanagedType.LPWStr)] [In] string szFieldName, [MarshalAs(UnmanagedType.Struct)] out object pvarField, out IntPtr ppvDataCArray);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void PutField([In] uint wFlags, [In] [Out] IntPtr pvData, [MarshalAs(UnmanagedType.LPWStr)] [In] string szFieldName, [MarshalAs(UnmanagedType.Struct)] [In] ref object pvarField);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void PutFieldNoCopy([In] uint wFlags, [In] [Out] IntPtr pvData, [MarshalAs(UnmanagedType.LPWStr)] [In] string szFieldName, [MarshalAs(UnmanagedType.Struct)] [In] ref object pvarField);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetFieldNames([In] [Out] ref uint pcNames, [MarshalAs(UnmanagedType.BStr)] out string rgBstrNames);

		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int IsMatchingType([MarshalAs(UnmanagedType.Interface)] [In] IRecordInfo pRecordInfo);

		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		IntPtr RecordCreate();

		[MethodImpl(MethodImplOptions.InternalCall)]
		void RecordCreateCopy([In] IntPtr pvSource, out IntPtr ppvDest);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void RecordDestroy([In] IntPtr pvRecord);
	}
}
