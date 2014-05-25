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
	/// Represents a video input.
	/// </summary>
	public class Input
	{
		#region Private Fields
		
		private Analog.Adapter adapter;
		private v4l2_input input;
		
		#endregion Private Fields
		
		#region Constructors and Destructors
		
        /// <summary>
        /// Creates a video input.
        /// </summary>
		/// <param name="adapter">The parental analog adapter.</param>
		/// <param name="input">The struct holding the video input information.</param>
		internal Input(Analog.Adapter adapter, v4l2_input input)
		{
			this.adapter = adapter;
			this.input = input;
		}
		
		#endregion Constructors and Destructors
		
		#region Public Properties
		
        /// <summary>
        /// Gets the video input's name.
        /// </summary>
		/// <value>The name of the video input.</value>
		public string Name
		{
			get { return input.name; }
		}
		
        /// <summary>
        /// Gets the type of the video input.
        /// </summary>
		/// <value>The type of the video input.</value>
		public InputType Type
		{
			get { return input.type; }
		}
		
        /// <summary>
        /// Gets the video input's tuner.
        /// </summary>
		/// <value>The tuner belonging to the video input.</value>
		public Analog.Tuner Tuner
		{
			// FIXME: inputs could have zero tuners!
			get { return adapter.Tuners[(int)input.tuner]; }
		}
		
        /// <summary>
        /// Gets the video input's current status.
        /// </summary>
		/// <value>The status of the video input.</value>
		public InputStatus Status
		{
			get { return input.status; }
		}
		
		/// <summary>
		/// Gets a bitmap of the video input's supported standards.
		/// </summary>
		/// <value>The bitmap of the supported standards.</value>
		public ulong SupportedStandards
		{
			get { return input.std; }
		}
		
		#endregion Public Properties
		
		#region Internal Properties
		
        /// <summary>
        /// Gets the video input's index in the list of all video inputs.
        /// </summary>
		/// <value>The index of the video input.</value>
		internal uint Index
		{
			get { return input.index; }
		}
		
		#endregion Internal Properties
	}
}