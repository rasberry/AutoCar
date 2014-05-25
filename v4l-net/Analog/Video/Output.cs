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
	/// Represents a video output.
	/// </summary>
	public class Output
	{
		#region Private Fields
		
		private v4l2_output output;
		
		#endregion Private Fields
		
		#region Constructors and Destructors
		
		internal Output(v4l2_output output)
		{
			this.output = output;
		}
		
		#endregion Constructors and Destructors
		
		#region Public Properties
		
		/// <summary>
		/// Gets the name of video output.
		/// </summary>
		/// <value>The name of the video output.</value>
		public string Name
		{
			get { return output.name; }
		}
		
		/// <summary>
		/// Gets the type of the video output.
		/// </summary>
		/// <value>The type of the video output.</value>
		public uint Type
		{
			get { return output.type; }
		}
		
		/// <summary>
		/// Gets the current status of the video output.
		/// </summary>
		/// <value>The current status.</value>
		public uint Status
		{
			get { return output.status; }
		}
		
		/// <summary>
		/// Gets a bitmap of the video output's supported video standards.
		/// </summary>
		/// <value>The bitmap of video standards.</value>
		public ulong SupportedStandards
		{
			get { return output.std; }
		}
		
		#endregion Public Properties
		
		#region Internal Properties
		
		internal uint Index
		{
			get { return output.index; }
		}
		
		#endregion Internal Properties
	}
}