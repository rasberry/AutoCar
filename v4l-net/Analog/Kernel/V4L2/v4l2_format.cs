#region LICENSE
/* 
 * Copyright (C) 2007 Tim Taubert (twenty-three@users.berlios.de)
 * 
 * This file is part of Video4Linux.Net.
 *
 * Video4Linux.Net is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 3 of the License, or
 * (at your option) any later version.
 *
 * Video4Linux.Net is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program. If not, see <http://www.gnu.org/licenses/>.
 */
#endregion LICENSE

using System;
using System.Runtime.InteropServices;

namespace Video4Linux.Analog.Kernel
{
	[StructLayout(LayoutKind.Sequential)]
	internal struct v4l2_format
	{
		public v4l2_buf_type type;
		public fmt_union fmt;
	}
	
	[StructLayout(LayoutKind.Explicit)]
	internal struct fmt_union
	{
		[FieldOffset(0)]
		public v4l2_pix_format pix;           // V4L2_BUF_TYPE_VIDEO_CAPTURE
		[FieldOffset(0)]
		public v4l2_window win;               // V4L2_BUF_TYPE_VIDEO_OVERLAY
		[FieldOffset(0)]
		public v4l2_vbi_format vbi;           // V4L2_BUF_TYPE_VBI_CAPTURE
		//[FieldOffset(0)]
		//public v4l2_sliced_vbi_format sliced; // V4L2_BUF_TYPE_SLICED_VBI_CAPTURE
		//[FieldOffset(0), MarshalAs(UnmanagedType.ByValArray, SizeConst=200)]
		//public byte[] raw;
	}

	//[StructLayout(LayoutKind.Explicit)]
	//internal struct fmt_union
	//{
	//	[FieldOffset(0)]
	//	public v4l2_pix_format pix;           // V4L2_BUF_TYPE_VIDEO_CAPTURE
	//	[FieldOffset(0)]
	//	public v4l2_pix_format_mplane pix_mp;
	//	[FieldOffset(0)]
	//	public v4l2_window win;               // V4L2_BUF_TYPE_VIDEO_OVERLAY
	//	[FieldOffset(0)]
	//	public v4l2_vbi_format vbi;           // V4L2_BUF_TYPE_VBI_CAPTURE
	//	[FieldOffset(0)]
	//	public v4l2_sliced_vbi_format sliced; // V4L2_BUF_TYPE_SLICED_VBI_CAPTURE
	//	[FieldOffset(0)]
	//	public v4l2_sdr_format sdr;
	//	[FieldOffset(0), MarshalAs(UnmanagedType.ByValArray, SizeConst=200)]
	//	public byte[] raw;
	//}
}
