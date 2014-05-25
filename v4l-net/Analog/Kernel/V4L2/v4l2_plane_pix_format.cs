using System;
using System.Runtime.InteropServices;

namespace Video4Linux.Analog.Kernel
{
	[StructLayout(LayoutKind.Sequential)]
	internal struct v4l2_plane_pix_format
	{
		public uint sizeimage;
		public ushort bytesperline; 
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=7)]
		public ushort reserved;
	}
}
