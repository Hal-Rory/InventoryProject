using Controllers;
using UnityEngine.Networking;

namespace Server
{
	public static class RequestManager
	{
		public static UnityWebRequest RequestUploadBuilder(string apiUrl, string apiEndpoint, string method, byte[] data, bool isJson = false)
		{
			UnityWebRequest request = new UnityWebRequest($"{apiUrl}/{apiEndpoint}");
			request.method = method;
			request.uploadHandler = new UploadHandlerRaw(data);
			request.downloadHandler = new DownloadHandlerBuffer();
			request.SetRequestHeader("Content-Type", isJson ? "application/json" : "text/plain");
			return request;
		}

		public static UnityWebRequest RequestQueryBuilder(string apiUrl, string apiEndpoint, string method, bool isJson = false)
		{
			UnityWebRequest request = new UnityWebRequest($"{apiUrl}/{apiEndpoint}");
			request.method = method;
			request.downloadHandler = new DownloadHandlerBuffer();
			request.SetRequestHeader("Content-Type", isJson ? "application/json" : "text/plain");
			return request;
		}

	}
}