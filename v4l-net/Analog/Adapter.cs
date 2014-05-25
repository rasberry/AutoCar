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

using Mono.Unix.Native;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

using Video4Linux.Analog.Kernel;

namespace Video4Linux.Analog
{
	/// <summary>
	/// Represents a Video4Linux hardware device.
	/// </summary>
	sealed public class Adapter
	{
		#region Private Fields
		
		private int deviceHandle;
		private Core.IOControl ioControl;
		
		private v4l2_capability? device;
		private uint bufferCount = 4;
		
		private Core.SearchableList<Analog.Audio.Input> audioInputs;
		private Core.SearchableList<Analog.Audio.Output> audioOutputs;
		private Core.SearchableList<Analog.Video.Input> inputs;
		private Core.SearchableList<Analog.Video.Output> outputs;
		private Core.SearchableList<Analog.Video.Standard> standards;
		private Core.SearchableList<Analog.Tuner> tuners;
		
		private List<Buffer> buffers = new List<Buffer>();
		private List<AdapterCapability> capabilities;
		private List<Analog.Video.Format> formats;
		
		private Analog.Video.Stream videoStream;
		private CaptureMethod captureMethod = CaptureMethod.MemoryMapping;
		
		#endregion Private Fields
		
		#region Constructors and Destructors
		
		/// <summary>
		/// Creates a Video4Linux device.
		/// </summary>
		/// <param name="path">Path to the device.</param>
		public Adapter(string path)
		{
			deviceHandle = Syscall.open(path, OpenFlags.O_RDWR);
			if (deviceHandle < 0)
				throw new Exception("Adapter " + path + " cannot be opened.");
			
			ioControl = new Core.IOControl(deviceHandle);
		}
		
		/// <summary>
		/// Destroys a Video4Linux device.
		/// </summary>
		~Adapter()
		{
			Syscall.close(deviceHandle);
		}
		
		#endregion Constructors and Destructors
		
		#region Private Methods
		
		/// <summary>
		/// Collects all available audio inputs from the device.
		/// </summary>
		private void fetchAudioInputs()
		{
			audioInputs = new Core.SearchableList<Analog.Audio.Input>();
			v4l2_audio cur = new v4l2_audio();
			
			cur.index = 0;
			while (ioControl.EnumerateAudioInputs(ref cur) == 0)
			{
				audioInputs.Add(new Analog.Audio.Input(this, cur));
				cur.index++;
			}
		}
		
		/// <summary>
		/// Collects all available audio outputs from the device.
		/// </summary>
		private void fetchAudioOutputs()
		{
			audioOutputs = new Core.SearchableList<Analog.Audio.Output>();
			v4l2_audioout cur = new v4l2_audioout();
			
			cur.index = 0;
			while (ioControl.EnumerateAudioOutputs(ref cur) == 0)
			{
				audioOutputs.Add(new Analog.Audio.Output(cur));
				cur.index++;
			}
		}
		
		/// <summary>
		/// Queries the device for its capabilites.
		/// </summary>
		private void fetchDevice()
		{
			v4l2_capability dv = new v4l2_capability();
			if (ioControl.QueryDeviceCapabilities(ref dv) < 0)
				throw new Exception("VIDIOC_QUERYCAP");
			
			device = dv;
		}
		
		/// <summary>
		/// Collects all available image formats from the device.
		/// </summary>
		private void fetchFormats()
		{
			formats = new List<Analog.Video.Format>();
			v4l2_fmtdesc cur = new v4l2_fmtdesc();
			
			cur.index = 0;
			while (ioControl.EnumerateFormats(ref cur) == 0)
			{
				formats.Add(new Analog.Video.Format(cur));
				cur.index++;
			}
		}
		
		/// <summary>
		/// Collects all available video inputs from the device.
		/// </summary>
		private void fetchInputs()
		{
			inputs = new Core.SearchableList<Analog.Video.Input>();
			v4l2_input cur = new v4l2_input();
			
			cur.index = 0;
			while (ioControl.EnumerateInputs(ref cur) == 0)
			{
				inputs.Add(new Analog.Video.Input(this, cur));
				cur.index++;
			}
		}
		
		/// <summary>
		/// Collects all available video outputs from the device.
		/// </summary>
		private void fetchOutputs()
		{
			outputs = new Core.SearchableList<Analog.Video.Output>();
			v4l2_output cur = new v4l2_output();
			
			cur.index = 0;
			while (ioControl.EnumerateOutputs(ref cur) == 0)
			{
				outputs.Add(new Analog.Video.Output(cur));
				cur.index++;
			}
		}
		
		/// <summary>
		/// Collects all available TV standards from the device.
		/// </summary>
		private void fetchStandards()
		{
			standards = new Core.SearchableList<Analog.Video.Standard>();
			v4l2_standard cur = new v4l2_standard();
			
			cur.index = 0;
			while (ioControl.EnumerateStandards(ref cur) == 0)
			{
				standards.Add(new Analog.Video.Standard(cur));
				cur.index++;
			}
		}
		
		/// <summary>
		/// Collects all available tuners from the device.
		/// </summary>
		private void fetchTuners()
		{
			tuners = new Core.SearchableList<Analog.Tuner>();
			v4l2_tuner cur = new v4l2_tuner();
			
			cur.index = 0;
			while (ioControl.GetTuner(ref cur) == 0)
			{
				tuners.Add(new Analog.Tuner(this, cur.index, cur.type));
				cur.index++;
			}
		}
		
		/// <summary>
		/// Enqueues all requested buffers.
		/// </summary>
		private void enqueueAllBuffers()
		{
			foreach (Analog.Buffer buf in buffers)
				buf.Enqueue();
		}
		
		/// <summary>
		/// Requests a given number of buffers for mmap data transfer.
		/// </summary>
		private void requestBuffers()
		{
			v4l2_requestbuffers req = new v4l2_requestbuffers();
			req.count = bufferCount;
			req.type = v4l2_buf_type.VideoCapture;
			req.memory = v4l2_memory.MemoryMapping;
			if (ioControl.RequestBuffers(ref req) < 0)
				throw new Exception("VIDIOC_REQBUFS");
				
			if (req.count < bufferCount)
				throw new Exception("VIDIOC_REQBUFS [count]");
			
			fetchBuffers(req);
		}
		
		/// <summary>
		/// Queries the device for information about each requested buffer.
		/// </summary>
		/// <param name="req">Struct with information about the request buffers.</param>
		private void fetchBuffers(v4l2_requestbuffers req)
		{
			for (uint i=0; i<req.count; i++)
			{
				v4l2_buffer buffer = new v4l2_buffer();
				buffer.index = i;
				buffer.type = req.type;
				buffer.memory = req.memory;
				if (ioControl.QueryBuffer(ref buffer) < 0)
					throw new Exception("VIDIOC_QUERYBUF");
				
				buffers.Add(new Analog.Buffer(this, buffer));
			}
		}
		
		private void fetchCapabilities()
		{
			if (device == null)
				fetchDevice();
			
			capabilities = new List<AdapterCapability>();
			
			foreach (object val in Enum.GetValues(typeof(AdapterCapability)))
				if ((((v4l2_capability)device).capabilities & (uint)val) != 0)
					capabilities.Add((AdapterCapability)val);
		}
		
		/// <summary>
		/// Gets a V4LStandard out of the list of all supported standards.
		/// </summary>
		/// <param name="std">Id of the standard.</param>
		private Analog.Video.Standard getStandardById(v4l2_std_id std)
		{
			foreach (Analog.Video.Standard standard in Standards)
				if (standard.Id == std)
					return standard;
			
			throw new Exception("VIDIOC_G_STD [std not in list]");
		}
		
		#endregion Private Methods
		
		#region Public Methods
		
		public int GetControlValue(Control ctrl)
		{
			v4l2_control cur = new v4l2_control();
			cur.id = (uint)ctrl;
			if (ioControl.GetControl(ref cur) < 0)
				throw new Exception("VIDIOC_G_CTRL");
			
			return cur.value;
		}
		
		public void SetControlValue(Control ctrl, int value)
		{
			v4l2_control cur = new v4l2_control();
			cur.id = (uint)ctrl;
			cur.value = value;
			if (ioControl.SetControl(ref cur) < 0)
				throw new Exception("VIDIOC_S_CTRL");
		}
		
		/// <summary>
		/// Starts the streaming I/O.
		/// </summary>
		public void StartStreaming()
		{
			// nothing to do here if we use read/write to capture
			if (captureMethod == CaptureMethod.ReadWrite)
				return;
			
			// request the streaming buffers if necessary
			if (buffers == null || buffers.Count != bufferCount)
				requestBuffers();
			
			// make sure that all buffers are in the incoming queue
			enqueueAllBuffers();
			
			v4l2_buf_type type = v4l2_buf_type.VideoCapture;
			if (ioControl.StreamingOn(ref type) < 0)
				throw new Exception("VIDIOC_STREAMON");
		}
		
		/// <summary>
		/// Stops the streaming I/O.
		/// </summary>
		public void StopStreaming()
		{
			// nothing to do here if we use read/write to capture
			if (captureMethod == CaptureMethod.ReadWrite)
				return;
			
			// destroy the buffers
			buffers = new List<Analog.Buffer>();
			
			v4l2_buf_type type = v4l2_buf_type.VideoCapture;
			if (ioControl.StreamingOff(ref type) < 0)
				throw new Exception("VIDIOC_STREAMOFF");
		}
		
		public void GetFormat(Analog.Video.BaseFormat fmt)
		{
			fmt.Get(this);
		}
		
		public void SetFormat(Analog.Video.BaseFormat fmt)
		{
			fmt.Set(this);
		}
		
		#endregion Public Methods
		
		#region Internal Properties
		
		/// <summary>
		/// Gets the file handle for the v4l device.
		/// </summary>
		/// <value>The file handle.</value>
		internal int DeviceHandle
		{
			get { return deviceHandle; }
		}
		
		internal Core.IOControl IoControl
		{
			get { return ioControl; }
		}
		
		#endregion Internal Properties
		
		#region Public Properties
		
		/// <summary>
		/// Gets the device's name.
		/// </summary>
		/// <value>The device's name.</value>
		public string Name
		{
			get
			{
				if (device == null)
					fetchDevice();
				
				return ((v4l2_capability)device).card;
			}
		}
		
		/// <summary>
		/// Gets the device driver's name.
		/// </summary>
		/// <value>The driver's name.</value>
		public string Driver
		{
			get
			{
				if (device == null)
					fetchDevice();
				
				return ((v4l2_capability)device).driver;
			}
		}
		
		/// <summary>
		/// Gets information about the bus the device is attached to.
		/// </summary>
		/// <value>The bus info string.</value>
		public string BusInfo
		{
			get
			{
				if (device == null)
					fetchDevice();
				
				return ((v4l2_capability)device).bus_info;
			}
		}
		
		public CaptureMethod CaptureMethod
		{
			get { return captureMethod; }
			set { captureMethod = value; }
		}
		
		/// <summary>
		/// Gets information about the device's capabilities.
		/// </summary>
		/// <value>The capability bitmap.</value>
		public ReadOnlyCollection<AdapterCapability> Capabilities
		{
			get
			{
				if (capabilities == null)
					fetchCapabilities();
				
				return capabilities.AsReadOnly();
			}
		}
		
		public Analog.Video.Stream VideoStream
		{
			get
			{
				if (videoStream == null)
					videoStream = new Analog.Video.Stream(this);
				
				return videoStream;
			}
		}
		
		/// <summary>
		/// Gets the device driver's version.
		/// </summary>
		/// <value>The version string.</value>
		public uint Version
		{
			get
			{
				if (device == null)
					fetchDevice();
				
				return ((v4l2_capability)device).version;
			}
		}
		
		/// <summary>
		/// Gets or sets the current audio input.
		/// </summary>
		/// <value>The audio input.</value>
		public Analog.Audio.Input AudioInput
		{
			get
			{
				v4l2_audio input = new v4l2_audio();
				if (ioControl.GetAudioInput(ref input) < 0)
					throw new Exception("VIDIOC_G_AUDIO");
				
				return AudioInputs[(int)input.index];
			}
			set
			{
				v4l2_audio input = value.ToStruct();
				if (ioControl.SetAudioInput(ref input) < 0)
					throw new Exception("VIDIOC_S_AUDIO");
			}
		}
		
		/// <summary>
		/// Gets or sets the current audio output.
		/// </summary>
		/// <value>The audio output.</value>
		public Analog.Audio.Output AudioOutput
		{
			get
			{
				v4l2_audioout output = new v4l2_audioout();
				if (ioControl.GetAudioOutput(ref output) < 0)
					throw new Exception("VIDIOC_G_AUDOUT");
				
				return AudioOutputs[(int)output.index];
			}
			set
			{
				v4l2_audioout output = value.ToStruct();
				if (ioControl.SetAudioOutput(ref output) < 0)
					throw new Exception("VIDIOC_S_AUDOUT");
			}
		}
		
		/// <summary>
		/// Gets or sets the number of buffers to use for streaming I/O with mmap.
		/// </summary>
		/// <value>The number of buffers to use.</value>
		public uint BufferCount
		{
			get { return bufferCount; }
			// TODO: must be immutable while capturing
			set { bufferCount = value; }
		}
		
		/// <summary>
		/// Gets or sets the current video input.
		/// </summary>
		/// <value>The video input.</value>
		public Analog.Video.Input Input
		{
			get
			{
				int idx = 0;
				if (ioControl.GetInput(ref idx) < 0)
					throw new Exception("VIDIOC_G_INPUT");
				
				return Inputs[idx];
			}
			set
			{
				int idx = (int)value.Index;
				if (ioControl.SetInput(ref idx) < 0)
					throw new Exception("VIDIOC_S_INPUT");
			}
		}
		
		/// <summary>
		/// Gets or sets the current video output.
		/// </summary>
		/// <value>The video output.</value>
		public Analog.Video.Output Output
		{
			get
			{
				int idx = 0;
				if (ioControl.GetOutput(ref idx) < 0)
					throw new Exception("VIDIOC_G_OUTPUT");
				
				return Outputs[idx];
			}
			set
			{
				int idx = (int)value.Index;
				if (ioControl.SetOutput(ref idx) < 0)
					throw new Exception("VIDIOC_S_OUTPUT");
			}
		}
		
		/// <summary>
		/// Gets or sets the current TV standard.
		/// </summary>
		/// <value>The TV standard.</value>
		public Analog.Video.Standard Standard
		{
			get
			{
				v4l2_std_id std = 0;
				if (ioControl.GetStandard(ref std) < 0)
					throw new Exception("VIDIOC_G_STD");
				
				return getStandardById(std);
			}
			set
			{
				v4l2_std_id std = value.Id;
				if (ioControl.SetStandard(ref std) < 0)
					throw new Exception("VIDIOC_S_STD");
			}
		}
		
		/// <summary>
		/// Gets all available audio inputs.
		/// </summary>
		/// <value>A list of audio inputs.</value>
		public Core.SearchableList<Analog.Audio.Input> AudioInputs
		{
			get
			{
				if (audioInputs == null)
					fetchAudioInputs();
				
				return audioInputs;
			}
		}
		
		/// <summary>
		/// Gets all available audio outputs.
		/// </summary>
		/// <value>A list of audio outputs.</value>
		public Core.SearchableList<Analog.Audio.Output> AudioOutputs
		{
			get
			{
				if (audioOutputs == null)
					fetchAudioOutputs();
				
				return audioOutputs;
			}
		}
		
		/// <summary>
		/// Gets the requested buffers for streaming I/O.
		/// </summary>
		/// <value>A list of buffers.</value>
		public List<Buffer> Buffers
		{
			get { return buffers; }
		}
		
		/// <summary>
		/// Gets all available image formats.
		/// </summary>
		/// <value>A list of image formats.</value>
		public List<Analog.Video.Format> Formats
		{
			get
			{
				if (formats == null)
					fetchFormats();
				
				return formats;
			}
		}
		
		/// <summary>
		/// Gets all available video inputs.
		/// </summary>
		/// <value>A list of video inputs.</value>
		public Core.SearchableList<Analog.Video.Input> Inputs
		{
			get
			{
				if (inputs == null)
					fetchInputs();
				
				return inputs;
			}
		}
		
		/// <summary>
		/// Gets all available video outputs.
		/// </summary>
		/// <value>A list of video outputs.</value>
		public Core.SearchableList<Analog.Video.Output> Outputs
		{
			get
			{
				if (outputs == null)
					fetchOutputs();
				
				return outputs;
			}
		}
		
		/// <summary>
		/// Gets all avaible TV standards.
		/// </summary>
		/// <value>A list of standards.</value>
		public Core.SearchableList<Analog.Video.Standard> Standards
		{
			get
			{
				if (standards == null)
					fetchStandards();
				
				return standards;
			}
		}
		
		/// <summary>
		/// Gets all available tuners.
		/// </summary>
		/// <value>A list of tuners.</value>
		public Core.SearchableList<Analog.Tuner> Tuners
		{
			get
			{
				if (tuners == null)
					fetchTuners();
				
				return tuners;
			}
		}
		
		#endregion Public Properties
	}
}