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

namespace Video4Linux.Analog.Kernel
{
	internal enum v4l2_buf_type : uint
	{
		// v4l2_pix_format
		VideoCapture       = 1,
		VideoOutput        = 2,
		// v4l2_window
		VideoOverlay       = 3,
		VideoOutputOverlay = 8, // TODO: ensure that v4l2_window is filled!
		// v4l2_vbi_format
		VBICapture         = 4,
		VBIOutput          = 5,
		// v4l2_sliced_vbi_format
		SlicedVBICapture   = 6,
		SlicedVBIOutput    = 7,

		VideoCaptureMplane = 9,
		VideoOutputMplane = 10,

		SDRCapture        = 11,

		// byte[] raw
		Private            = 0x80
	}
}
