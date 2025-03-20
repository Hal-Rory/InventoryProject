using Controllers;
using UnityEngine.Networking;

namespace Server
{
	public static class RequestManager
	{
		private static string _apiUrl => GameController.Instance.ConfigLoader.Config.ApiUrl;

		public static UnityWebRequest RequestUploadBuilder(string apiEndpoint, string method, byte[] data, bool isJson = false)
		{
			UnityWebRequest request = new UnityWebRequest($"{_apiUrl}/{apiEndpoint}");
			request.method = method;
			request.uploadHandler = new UploadHandlerRaw(data);
			request.downloadHandler = new DownloadHandlerBuffer();
			request.SetRequestHeader("Content-Type", isJson ? "application/json" : "text/plain");
			return request;
		}

		public static UnityWebRequest RequestQueryBuilder(string apiEndpoint, string method, bool isJson = false)
		{
			UnityWebRequest request = new UnityWebRequest($"{_apiUrl}/{apiEndpoint}");
			request.method = method;
			request.downloadHandler = new DownloadHandlerBuffer();
			request.SetRequestHeader("Content-Type", isJson ? "application/json" : "text/plain");
			return request;
		}

	}
}