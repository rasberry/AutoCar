using System;
using System.Runtime.InteropServices;

using Video4Linux.Analog.Kernel;

namespace Video4Linux.Analog.Video
{
	public class Stream : System.IO.Stream, IDisposable
	{
		private Analog.Adapter adapter;
		
		public Stream(Analog.Adapter adapter)
			: base()
		{
			this.adapter = adapter;
		}
		
		public override int Read(byte[] buffer, int offset, int count)
		{
			switch (adapter.CaptureMethod)
			{
			case CaptureMethod.MemoryMapping:
				return readMM(buffer, offset, count);
			case CaptureMethod.ReadWrite:
				return readRW(buffer, offset, count);
			case CaptureMethod.UserPointer:
				return readUP(buffer, offset, count);
			default:
				throw new Exception("Unsupported capture method.");
			}
		}
		
		/// HACK: we should read until we reached count
		/// HACK: offset is ignored
		private int readMM(byte[] buffer, int offset, int count)
		{
			if (!adapter.Capabilities.Contains(AdapterCapability.Streaming))
				throw new Exception("Device does not support streaming.");
			
			v4l2_buffer buf = new v4l2_buffer();
			buf.type = adapter.Buffers[0].Type;
			buf.memory = adapter.Buffers[0].Memory;
			
			// read one image
			if (adapter.IoControl.DequeueBuffer(ref buf) == 0)
			{
				Analog.Buffer dbuf = adapter.Buffers[(int)buf.index];
				
				// max length = buffer length
				if ((int)dbuf.Length < count)
					count = (int)dbuf.Length;
				
				// copy all the data from the buffer
				Marshal.Copy(dbuf.Start, buffer, 0, count);
				
				// re-enqueue the buffer
				dbuf.Enqueue();
				
				return count;
			}
			
			return 0;
		}
		
		private int readRW(byte[] buffer, int offset, int count)
		{
			if (!adapter.Capabilities.Contains(AdapterCapability.ReadWrite))
				throw new Exception("Device does not support read/write.");
			
			IntPtr buf = Marshal.AllocHGlobal(offset + count);
			long length = Mono.Unix.Native.Syscall.read(adapter.DeviceHandle, buf, (ulong)(offset + count));
			
			Marshal.Copy(buf, buffer, offset, count);
			
			return (int)length;
		}
		
		/// TODO: implement in some far future
		private int readUP(byte[] buffer, int offset, int count)
		{
			if (!adapter.Capabilities.Contains(AdapterCapability.Streaming))
				throw new Exception("Device does not support streaming.");
			
			throw new NotImplementedException();
		}
		
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}
		
		public override long Seek(long offset, System.IO.SeekOrigin origin)
		{
			throw new NotSupportedException();
		}
		
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}
		
		public override void Flush()
		{}
		
		public override bool CanRead
		{
			get { return true; }
		}
		
		public override bool CanSeek
		{
			get { return false; }
		}
		
		public override bool CanWrite
		{
			get { return false; }
		}
		
		public override long Position
		{
			get { throw new NotSupportedException(); }
			set { throw new NotSupportedException(); }
		}
		
		public override long Length
		{
			get { throw new NotSupportedException(); }
		}
	}
}
