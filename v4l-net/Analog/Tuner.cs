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

namespace Video4Linux.Analog
{
	/// <summary>
	/// Represents a tuner.
	/// </summary>
	public class Tuner
	{
		#region Private Fields
		
		private Adapter adapter;
		private v4l2_tuner tuner;
		
		#endregion Private Fields
		
		#region Constructors and Destructors
		
        /// <summary>
        /// Creates a tuner.
        /// </summary>
		/// <param name="device">The parental Video4Linux device.</param>
		/// <param name="index">The index of the tuner.</param>
		/// <param name="type">The type of the tuner.</param>
		internal Tuner(Adapter adapter, uint index, TunerType type)
		{
			this.adapter = adapter;
			
			tuner = new v4l2_tuner();
			tuner.index = index;
			tuner.type = type;
			getTuner();
		}
		
		#endregion Constructors and Destructors
		
		#region Private Methods
		
		private void getTuner()
		{
			if (adapter.IoControl.GetTuner(ref tuner) < 0)
				throw new Exception("VIDIOC_G_TUNER");
		}
		
		#endregion Private Methods
		
		#region Public Properties
		
        /// <summary>
        /// Gets the tuner's name.
        /// </summary>
		/// <value>The name of the tuner.</value>
		public string Name
		{
			get { return tuner.name; }
		}
		
        /// <summary>
        /// Gets the tuner's type.
        /// </summary>
		/// <value>The type of the tuner.</value>
		public TunerType Type
		{
			get { return tuner.type; }
		}
		
		/// <summary>
		/// Gets a bitmap of the tuner's capabilities.
		/// </summary>
		/// <value>A bitmap of the tuner's capabilities.</value>
		public uint Capabilities
		{
			get { return tuner.capability; }
		}
		
        /// <summary>
        /// Gets or sets the tuner's current frequency.
        /// </summary>
		/// <value>The frequency.</value>
		public uint Frequency
		{
			get
			{
				v4l2_frequency freq = new v4l2_frequency();
				freq.tuner = tuner.index;
				freq.type = tuner.type;
				if (adapter.IoControl.GetFrequency(ref freq) < 0)
					throw new Exception("VIDIOC_G_FREQUENCY");
				
				return freq.frequency;
			}
			// TODO: if AFC is on, then negotiate the right frequency
			set 
			{
				v4l2_frequency freq = new v4l2_frequency();
				freq.tuner = tuner.index;
				freq.type = tuner.type;
				freq.frequency = value;
				if (adapter.IoControl.SetFrequency(ref freq) < 0)
					throw new Exception("VIDIOC_S_FREQUENCY");
			}
		}
		
        /// <summary>
        /// Gets the lowest possible tuner frequency.
        /// </summary>
		/// <value>The frequency.</value>
		public uint LowestFrequency
		{
			get { return tuner.rangelow; }
		}
		
        /// <summary>
        /// Gets the highest possible tuner frequency.
        /// </summary>
		/// <value>The frequency.</value>
		public uint HighestFrequency
		{
			get { return tuner.rangehigh; }
		}
		
        /// <summary>
        /// Gets the tuner's signal quality.
        /// </summary>
		/// <value>The signal quality.</value>
		public uint Signal
		{
			get
			{
				getTuner();
				return tuner.signal;
			}
		}
		
		/// <summary>
		/// Gets the tuner's current automatic frequency control value.
		/// </summary>
		/// <value>The automatic frequency control value.</value>
		public int AFC
		{
			get
			{
				getTuner();
				return tuner.afc;
			}
		}
		
		/// <summary>
		/// Gets the tuner's audio mode.
		/// </summary>
		/// <value>The audio mode.</value>
		public TunerAudioMode AudioMode
		{
			get { return tuner.audmode; }
		}
		
		#endregion Public Properties
		
		#region Internal Properties
		
		/// <summary>
		/// Gets the index of this tuner in the list of all available tuners.
		/// </summary>
		/// <value>The index of this tuner.</value>
		internal uint Index
		{
			get { return tuner.index; }
		}
		
		#endregion Internal Properties
	}
}