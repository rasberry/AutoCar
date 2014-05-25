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

namespace Video4Linux.Analog.Kernel.V4L1
{
	public enum video_type : int
	{
		/* Can capture to memory */
		VID_TYPE_CAPTURE	= 1,
		/* Has a tuner of some form */
		VID_TYPE_TUNER		= 2,
		/* Has teletext capability */
		VID_TYPE_TELETEXT	= 4,
		/* Can overlay its image onto the frame buffer */
		VID_TYPE_OVERLAY	= 8,
		/* Overlay is Chromakeyed */
		VID_TYPE_CHROMAKEY	= 16,
		/* Overlay clipping is supported */
		VID_TYPE_CLIPPING	= 32,
		/* Overlay overwrites frame buffer memory */
		VID_TYPE_FRAMERAM	= 64,
		/* The hardware supports image scaling */
		VID_TYPE_SCALES		= 128,
		/* Image capture is grey scale only */
		VID_TYPE_MONOCHROME	= 256,
		/* Capture can be of only part of the image */
		VID_TYPE_SUBCAPTURE	= 512
	}
}
