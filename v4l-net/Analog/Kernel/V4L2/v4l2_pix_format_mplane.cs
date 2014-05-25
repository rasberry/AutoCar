using System;
using System.Runtime.InteropServices;

namespace Video4Linux.Analog.Kernel
{
	[StructLayout(LayoutKind.Sequential)]
	internal struct v4l2_pix_format_mplane
	{
		public uint width;
		public uint height;
		public uint pixelformat;
		public v4l2_field field;
		public v4l2_colorspace colorspace;
		[MarshalAs(UnmanagedType.ByValArray)]
		public v4l2_plane_pix_format[] plane_fmt;
		public byte num_planes;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
		public byte[] reseved;
	}
}
