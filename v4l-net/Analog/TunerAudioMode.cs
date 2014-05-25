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

namespace Video4Linux.Analog
{
	/// <summary>
	/// Represents a tuner's audio mode.
	/// </summary>
	public enum TunerAudioMode : uint
	{
		/// <summary>
		/// Play mono audio. When the tuner receives a stereo signal this a down-mix of
		/// the left and right channel. When the tuner receives a bilingual or SAP signal
		/// this mode selects the primary language.
		/// </summary>
		Mono        = 0,
		
		/// <summary>
		/// Play stereo audio. When the tuner receives bilingual audio it may play
		/// different languages on the left and right channel or the primary language on both
		/// channels.
		/// </summary>
		Stereo      = 1,
		
		/// <summary>
		/// Play the primary language, mono or stereo. Only analog TV tuners support this mode.
		/// </summary>
		Lang1       = 3,
		
		/// <summary>
		/// Play the secondary language, mono. When the tuner receives no bilingual audio or
		/// SAP, or their reception is not supported the driver shall fall back to mono or
		/// stereo mode. Only analog TV tuners support this mode.
		/// </summary>
		Lang2       = 2,
		
		/// <summary>
		/// Play the primary language on the left channel, the secondary language on the right channel. Only analog TV tuners support this mode.
		/// </summary>
		Lang1_Lang2 = 4
	}
}
