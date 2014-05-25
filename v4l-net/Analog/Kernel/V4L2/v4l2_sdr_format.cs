using System;
using System.Runtime.InteropServices;

namespace Video4Linux.Analog.Kernel
{
	[StructLayout(LayoutKind.Sequential)]
	internal struct v4l2_sdr_format
	{
		public uint pixelformat;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=28)]
		public byte[] reserved;
	}
}
