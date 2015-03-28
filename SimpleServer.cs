using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//http://www.codeproject.com/Articles/137979/Simple-HTTP-Server-in-C

namespace AutoCar
{
	class SimpleServer : HttpServer
	{
		public SimpleServer(int port) : base(port) {}
		public VideoGrabber ImgSource;

		public override void handleGETRequest(HttpProcessor p)
		{
			if (p.http_url.StartsWith("/disp")) {
				byte[] data = ImgSource.GetImage();
				p.httpResponseHeaders["Cache-Control"] = "no-cache, must-revalidate";
				p.writeSuccess("image/jpeg");
				p.outputStream.Flush();
				p.outputStream.BaseStream.Write(data,0,data.Length);
				p.outputStream.BaseStream.Flush();
			}
			else if (p.http_url.StartsWith("/left")) {
				byte[] data = ImgSource.GetLeftImage();
				p.httpResponseHeaders["Cache-Control"] = "no-cache, must-revalidate";
				p.writeSuccess("image/jpeg");
				p.outputStream.Flush();
				p.outputStream.BaseStream.Write(data,0,data.Length);
				p.outputStream.BaseStream.Flush();
			}
			else if (p.http_url.StartsWith("/right")) {
				byte[] data = ImgSource.GetRightImage();
				p.httpResponseHeaders["Cache-Control"] = "no-cache, must-revalidate";
				p.writeSuccess("image/jpeg");
				p.outputStream.Flush();
				p.outputStream.BaseStream.Write(data,0,data.Length);
				p.outputStream.BaseStream.Flush();
			}
			else if (p.http_url == "/main.css") {
				p.writeSuccess("text/css");
				string txt = File.ReadAllText("Server/main.css");
				p.outputStream.Write(txt);
			}
			else if (p.http_url == "/jquery.js") {
				p.writeSuccess("text/javascript");
				string txt = File.ReadAllText("Server/jquery.js");
				p.outputStream.Write(txt);

			}
			else if (p.http_url == "/main.js") {
				p.writeSuccess("text/javascript");
				string txt = File.ReadAllText("Server/main.js");
				p.outputStream.Write(txt);
			}
			else if (p.http_url == "" || p.http_url == "/" || p.http_url == "/index") {
				p.writeSuccess();
				string txt = File.ReadAllText("Server/main.html");
				p.outputStream.Write(txt);
			} else {
				p.writeSuccess();
				p.outputStream.Write(p.http_url);
			}
		}

		public override void handlePOSTRequest(HttpProcessor p, System.IO.StreamReader inputData)
		{
			
		}
	}
}