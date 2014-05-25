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
	/// <summary>
	/// Holds all possible operation to be executed on the driver.
	/// </summary>
	internal enum v4l2_operation : int
	{
		// HACK: check for _IOR/_IOWR
		QueryCapabilities     = -2140645888, // VIDIOC_QUERYCAP
		GetStandard           = -2146937321, // VIDIOC_G_STD
		SetStandard           =  1074288152, // VIDIOC_S_STD
		GetInput              = -2147199450, // VIDIOC_G_INPUT
		SetInput              = -1073457625, // VIDIOC_S_INPUT
		GetOutput             = -2147199442, // VIDIOC_G_OUTPUT
		SetOutput             = -1073457617, // VIDIOC_S_OUTPUT
		GetAudioInput         = -2144053727, // VIDIOC_G_AUDIO
		SetAudioInput         =  1077171746, // VIDIOC_S_AUDIO
		GetAudioOutput        = -2144053711, // VIDIOC_G_AUDOUT
		SetAudioOutput        =  1077171762, // VIDIOC_S_AUDOUT
		GetFormat             = -1060350460, // VIDIOC_G_FMT
		SetFormat             = -1060350459, // VIDIOC_S_FMT
		GetTuner              = -1068214755, // VIDIOC_G_TUNER
		SetTuner              =  1079268894, // VIDIOC_S_TUNER
		GetFrequency          = -1070836168, // VIDIOC_G_FREQUENCY
		SetFrequency          =  1076647481, // VIDIOC_S_FREQUENCY
		GetFramebuffer        = -2144578038, // VIDIOC_G_FBUF
		SetFramebuffer        =  1076647435, // VIDIOC_S_FBUF
		GetControl            = -1073195493, // VIDIOC_G_CTRL
		SetControl            = -1073195492, // VIDIOC_S_CTRL
		RequestBuffers        = -1072409080, // VIDIOC_REQBUFS
		QueryBuffer           = -1069263351, // VIDIOC_QUERYBUF
		StreamingOn           =  1074026002, // VIDIOC_STREAMON
		StreamingOff          =  1074026003, // VIDIOC_STREAMOFF
		EnqueueBuffer         = -1069263345, // VIDIOC_QBUF
		DequeueBuffer         = -1069263343, // VIDIOC_DQBUF
		EnumerateInputs       = -1068739046, // VIDIOC_ENUMINPUT
		EnumerateOutputs      = -1069001168, // VIDIOC_ENUMOUTPUT
		EnumerateAudioInputs  = -1070311871, // VIDIOC_ENUMAUDIO
		EnumerateAudioOutputs = -1070311870, // VIDIOC_ENUMAUDOUT
		EnumerateStandards    = -1069525479, // VIDIOC_ENUMSTD
		EnumerateFormats      = -1069525502  // VIDIOC_ENUM_FMT
	}
}
