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
	/// Represents a video capture and output format.
	/// </summary>
	abstract public class VideoFormat : BaseFormat
	{
		#region Constructors and Destructors
		
		/// <summary>
		/// Creates a video capture/output format.
		/// </summary>
		public VideoFormat()
			: this(352, 288)
		{}
		
		public VideoFormat(uint width, uint height)
			: base()
		{
			format.fmt.pix.width = width;
			format.fmt.pix.height = height;
		}
		
		#endregion Constructors and Destructors
		
		#region Public Properties
		
		/// <summary>
		/// Gets the image width in pixels.
		/// </summary>
		public uint Width
		{
			get { return format.fmt.pix.width; }
			set { format.fmt.pix.width = value; }
		}
		
		/// <summary>
		/// Gets the image height in pixels.
		/// </summary>
		/// <value>The image height.</value>
		public uint Height
		{
			get { return format.fmt.pix.height; }
			set { format.fmt.pix.height = value; }
		}
		
		/// <summary>
		/// Gets the size of a line in bytes.
		/// </summary>
		/// <value>The bytes per line.</value>
		public uint BytesPerLine
		{
			get { return format.fmt.pix.bytesperline; }
			set { format.fmt.pix.bytesperline = value; }
		}
		
		public v4l2_field Field
		{
			get { return format.fmt.pix.field; }
			set { format.fmt.pix.field = value; }
		}
		
		/// <summary>
		/// Gets or sets the image's pixel format.
		/// </summary>
		/// <value>The pixel format.</value>
		public v4l2_pix_format_id PixelFormat
		{
			get { return format.fmt.pix.pixelformat; }
			set { format.fmt.pix.pixelformat = value; }
		}
		
		public uint SizeImage
		{
			get { return format.fmt.pix.sizeimage; }
		}
		
		/*
		 * public v4l2_colorspace colorspace [get]
		 */
		
		#endregion Public Properties
	}
}
