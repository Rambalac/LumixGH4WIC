using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WIC
{
	[Guid("00000040-A8F2-4877-BA0A-FD2B6645FB94"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IWICPalette
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void InitializePredefined([In] WICBitmapPaletteType ePaletteType, [In] int fAddTransparentColor);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void InitializeCustom([In] ref uint pColors, [In] uint cCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void InitializeFromBitmap([MarshalAs(UnmanagedType.Interface)] [In] IWICBitmapSource pISurface, [In] uint cCount, [In] int fAddTransparentColor);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void InitializeFromPalette([MarshalAs(UnmanagedType.Interface)] [In] IWICPalette pIPalette);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetType(out WICBitmapPaletteType pePaletteType);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetColorCount(out uint pcCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetColors([In] uint cCount, out uint pColors, out uint pcActualColors);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void IsBlackWhite(out int pfIsBlackWhite);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void IsGrayscale(out int pfIsGrayscale);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void HasAlpha(out int pfHasAlpha);
	}
}
