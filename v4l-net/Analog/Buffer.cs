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
using System.Runtime.InteropServices;

using Video4Linux.Analog.Kernel;

namespace Video4Linux.Analog
{
    /// <summary>
	/// Represents a Video4Linux buffer used with streaming I/O and mmap
	/// and stores (info about) the captured frames.
    /// </summary>
	public class Buffer
	{
		#region Private Fields
		
		private Analog.Adapter adapter;
		private v4l2_buffer buffer;
		private IntPtr start;
		private uint originalLength;
		
		#endregion Private Fields
		
		#region Constructors and Destructors
		
		/// <summary>
		/// Creates a buffer to be used with Video4Linux streaming I/O.
		/// </summary>
		/// <param name="adapter">The parental Video4Linux device.</param>
		/// <param name="buffer">The struct holding the buffer information.</param>
		internal Buffer(Analog.Adapter adapter, v4l2_buffer buffer)
		{
			this.adapter = adapter;
			this.buffer = buffer;
			
			mapMemory();
		}
		
		/// <summary>
		/// Extends the destruction of a Video4Linux buffer.
		/// </summary>
		~Buffer()
		{
			unmapMemory();
		}
		
		#endregion Constructors and Destructors
		
		#region Private Methods
		
		/// <summary>
		/// Maps the memory belonging to the buffer.
		/// </summary>
		private void mapMemory()
		{
			start = Syscall.mmap
				(IntPtr.Zero,
				 buffer.length,
				 MmapProts.PROT_READ | MmapProts.PROT_WRITE,
				 MmapFlags.MAP_SHARED,
				 adapter.DeviceHandle,
				 buffer.m.offset);
			originalLength = buffer.length;
			if (start == Syscall.MAP_FAILED)
				throw new Exception("Memory mapping failed.");
		}
		
		/// <summary>
		/// Unmaps the memory belonging to the buffer.
		/// </summary>
		private void unmapMemory()
		{
			Mono.Unix.Native.Syscall.munmap(start, originalLength);
		}
		
		#endregion Private Methods
		
		#region Public Methods
		
		/// <summary>
		/// Puts the buffer into the driver's incoming queue.
		/// </summary>
		internal void Enqueue()
		{
			if (adapter.IoControl.EnqueueBuffer(ref buffer) < 0)
				throw new Exception("VIDIOC_QBUF");
		}
		
		/// <summary>
		/// Removes the buffer from the driver's outgoing queue.
		/// </summary>
		internal void Dequeue()
		{
			if (adapter.IoControl.DequeueBuffer(ref buffer) < 0)
				throw new Exception("VIDIOC_DQBUF");
		}
		
		#endregion Public Methods
		
		#region Public Properties
		
		/// <summary>
		/// Gets the number of bytes that are currently used.
		/// </summary>
		/// <value>The number of bytes currently in use.</value>
		public uint BytesUsed
		{
			get { return buffer.bytesused; }
		}
		
		public uint Offset
		{
			get { return buffer.m.offset; }
		}
		
		/// <summary>
		/// Gets the length of the buffer in bytes.
		/// </summary>
		/// <value>The length of the buffer in bytes.</value>
		public uint Length
		{
			get { return buffer.length; }
		}
		
		public uint Sequence
		{
			get { return buffer.sequence; }
		}
		
		public IntPtr Start
		{
			get { return start; }
		}
		
		#endregion Public Properties
		
		#region Internal Properties
		
		/// <summary>
		/// Gets the index of this buffer in the list of all buffers.
		/// </summary>
		/// <value>The index of this buffer.</value>
		internal uint Index
		{
			get { return buffer.index; }
		}
		
		internal v4l2_buf_type Type
		{
			get { return buffer.type; }
		}
		
		internal v4l2_memory Memory
		{
			get { return buffer.memory; }
		}
		
		#endregion Internal Properties
	}
}