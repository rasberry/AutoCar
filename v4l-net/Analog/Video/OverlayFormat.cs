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
	/// <summary>
	/// Represents a video overlay format.
	/// </summary>
	abstract public class OverlayFormat : BaseFormat
	{
		#region Constructors and Destructors
		
		internal OverlayFormat()
			: base()
		{}
		
		/// <summary>
		/// Creates a video overlay format.
		/// </summary>
		internal OverlayFormat(Analog.Video.Rectangle win)
			: base()
		{
			format.fmt.win.w = win.ToStruct();
		}
		
		#endregion Constructors and Destructors
		
		#region Public Properties
		
		/// <summary>
		/// Gets or sets the clipping rectangle.
		/// </summary>
		/// <value>The rectangle to be clipped.</value>
		public Analog.Video.Rectangle Window
		{
			get { return new Analog.Video.Rectangle(format.fmt.win.w); }
			set { format.fmt.win.w = value.ToStruct(); }
		}
		
		/*
		 * field
		 * chromakey
		 * clips
		 * clipcount
		 * bitmap
		 * global_alpha
		 */
		
		#endregion Public Properties
	}
}