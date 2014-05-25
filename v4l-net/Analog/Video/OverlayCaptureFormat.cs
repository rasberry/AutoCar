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

using Video4Linux.Analog.Kernel;

namespace Video4Linux.Analog.Video
{
	sealed public class OverlayCaptureFormat : OverlayFormat
	{
		#region Constructors and Destructors
		
		public OverlayCaptureFormat()
			: base()
		{
			format.type = v4l2_buf_type.VideoOverlay;
		}
		
		public OverlayCaptureFormat(Analog.Video.Rectangle win)
			: base(win)
		{
			format.type = v4l2_buf_type.VideoOverlay;
		}
		
		#endregion Constructors and Destructors
	}
}