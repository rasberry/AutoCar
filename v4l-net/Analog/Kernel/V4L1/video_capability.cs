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

namespace Video4Linux.Analog.Kernel.V4L1
{
	internal struct video_capability
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst=32)]
		public string name;
		public int type;
		/* Number of channels */
		public int channels;
		/* Number of audio devices */
		public int audios;
		/* Maximum supported width */
		public int maxwidth;
		/* Maximum supported height */
		public int maxheight;
		/* Minimum supported width */
		public int minwidth;
		/* Minimum supported height */
		public int minheight;
	}
}
