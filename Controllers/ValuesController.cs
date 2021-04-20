using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.Ajax.Utilities;
using Microsoft.Web.WebSockets;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebSocketServerAPI.Controllers
{
	public class ValuesController : ApiController
	{
		public HttpResponseMessage Get()
		{
			if (HttpContext.Current.IsWebSocketRequest)
			{
				HttpContext.Current.AcceptWebSocketRequest(new ChatWebSocketHandler());
				return Request.CreateResponse(HttpStatusCode.SwitchingProtocols);
			}
			else {
				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}		
		}

		class ChatWebSocketHandler : Microsoft.Web.WebSockets.WebSocketHandler
		{
			private static WebSocketCollection _chatClients = new WebSocketCollection();

			public override void OnOpen()
			{
				_chatClients.Add(this);
			}

			public override void OnMessage(string Requestdata)
			{
				Task.Run(() =>
				{
					if (Requestdata != "")
					{
						var json = JObject.Parse(Requestdata);
						var result = Task.Run<string>(() => DoGet("https://localhost:44348/" + json["url"].ToString())).GetAwaiter().GetResult();
						Send("SERVER ONMESSAGE > '" + result + " => " + json["name"].ToString());
					}
				});
			}
			public override void OnClose()
			{
				Send("SERVER ONCLOSE > DISCONNECTED ");
			}
			public override void OnError()
			{
				Send("SERVER ONERROR > ERROR ");
			}

			private HttpClient GetHttpClient()
			{
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
				HttpClient _client = new HttpClient();
				_client.DefaultRequestHeaders.Accept.Clear();
				_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				//_client.DefaultRequestHeaders.Add("X-Authorization", "Bearer " + oauthToken);
				return _client;
			}
			public async System.Threading.Tasks.Task<string> DoGet(String url, string JWTToken = "")
			{
				string jsonResult = "";

				if (string.IsNullOrEmpty(JWTToken))
				{
					var res = GetHttpClient().GetAsync(url).Result;
					jsonResult = await res.Content.ReadAsStringAsync();
				}
				else
				{
					//var res = GetHttpClientForJWT(JWTToken).GetAsync(url).Result;
					//jsonResult = await res.Content.ReadAsStringAsync();
				}

				return jsonResult;
			}

			//public async Task<string> DoPost(String url, String body, string JWTToken = "")
			//{
			//	string jsonResult = "";

			//	if (string.IsNullOrEmpty(JWTToken))
			//	{
			//		var res = GetHttpClient().SendRequest(url, body, "POST").Result;
			//		jsonResult = await res.Content.ReadAsStringAsync();
			//	}
			//	else
			//	{
			//		var content = new StringContent(body);
			//		var res = GetHttpClientForJWT(JWTToken).PostAsync(url, body).Result;
			//		var res = GetHttpClientForJWT(JWTToken).SendRequest(url, body, "POST").Result;
			//		var buffer = System.Text.Encoding.UTF8.GetBytes(body);
			//		var byteContent = new ByteArrayContent(buffer);
			//		byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			//		var res = GetHttpClientForJWT(JWTToken).PostAsync(url, byteContent).Result;

			//		jsonResult = await res.Content.ReadAsStringAsync();
			//	}

			//	return jsonResult;
			//}


		}
	}
}
