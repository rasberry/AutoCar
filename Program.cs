using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using Video4Linux.Analog;
using Video4Linux.Analog.Video;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Threading;

namespace AutoCar
{
	class Program
	{
		static void Main(string[] args)
		{
			SimpleServer server = null;
			VideoGrabber hand = null;

			try {
				hand = new VideoGrabber(Config.DeviceOne,Config.DeviceTwo);
				Thread vidworker = new Thread(new ThreadStart(hand.Start));
				vidworker.Start();

				server = new SimpleServer(Config.Port);
				server.ImgSource = hand;
				Thread webworker = new Thread(new ThreadStart(server.listen));
				webworker.Start();

				WL("Server started on port "+Config.Port+". Press any key to shutdown");
				ConsoleKeyInfo key = Console.ReadKey(true);
			}
			finally {
				if (server != null) { server.stop(); }
				if (hand != null) { hand.Stop(); }
			}
		}

		static void Info(Adapter adapter)
		{
			WL("= Capabilities");
			foreach(AdapterCapability cap in adapter.Capabilities)
			{
				WL(cap.ToString());
			}
			WL("= Driver");
			WL(adapter.Driver);

			WL("= BusInfo");
			WL(adapter.BusInfo);

			//VideoCaptureFormat format = new VideoCaptureFormat();
			//adapter.GetFormat(format);
			//WL("= vc "+format.Width+" "+format.Height+" "+format.PixelFormat);
			//adapter.SetFormat(format);

			WL("=Inputs");
			foreach(Input inp in adapter.Inputs)
			{
				WL(inp.Name+" "+inp.Status+" "+inp.Type+" "+inp.SupportedStandards);
			}

			WL("=Outputs");
			foreach(Output outp in adapter.Outputs)
			{
				WL(outp.Name+" "+outp.Status+" "+outp.Type+" "+outp.SupportedStandards);
			}

			WL("=Standard");
			foreach(Standard std in adapter.Standards)
			{
				WL(std.Name+" "+std.FrameLines);
			}
		}

		static void WL(string message)
		{
			Console.WriteLine(message);
		}
	}
}
