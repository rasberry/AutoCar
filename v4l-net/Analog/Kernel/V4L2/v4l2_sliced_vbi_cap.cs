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
	internal struct v4l2_sliced_vbi_cap
	{
		public ushort service_set;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=48)]
		public ushort[] service_lines;
		public v4l2_buf_type type;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=3)]
		public uint[] reserved;
	}
}