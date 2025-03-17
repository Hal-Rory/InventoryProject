using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Server
{
	public class ConfigLoader : MonoBehaviour
	{
		[SerializeField] private string _apiUrl = "";
		[SerializeField] private string _configUrl = "";

		public static ConfigData Config { get; private set; }

		public Dictionary<EndPoints, string> InventoryEndpoints;
		public Dictionary<EndPoints, string> ItemEndpoints;
		public Dictionary<EndPoints, string> PlayerEndpoints;

		private void Awake()
		{
			StartCoroutine(LoadConfig());
		}

		private IEnumerator LoadConfig()
		{
			UnityWebRequest request = UnityWebRequest.Get($"{_apiUrl}/{_configUrl}");
			yield return request.SendWebRequest();

			if (request.result == UnityWebRequest.Result.Success)
			{
				Config = JsonConvert.DeserializeObject<ConfigData>(request.downloadHandler.text);
			}

			Config.ApiUrl = _apiUrl;

			InventoryEndpoints = new Dictionary<EndPoints, string>
			{
				{ EndPoints.Create, string.Format(Config.Inventory.ControllerPath, Config.ApiVersion, Config.CreateKeyword, Config.Inventory.Endpoints.Create) },
				{ EndPoints.Get, string.Format(Config.Inventory.ControllerPath, Config.ApiVersion, Config.CreateKeyword, Config.Inventory.Endpoints.Get) },
				{ EndPoints.GetAll, string.Format(Config.Inventory.ControllerPath, Config.ApiVersion, Config.CreateKeyword, Config.Inventory.Endpoints.GetAll) },
				{ EndPoints.Update, string.Format(Config.Inventory.ControllerPath, Config.ApiVersion, Config.CreateKeyword, Config.Inventory.Endpoints.Update) },
				{ EndPoints.Delete, string.Format(Config.Inventory.ControllerPath, Config.ApiVersion, Config.CreateKeyword, Config.Inventory.Endpoints.Delete) }
			};

			ItemEndpoints = new Dictionary<EndPoints, string>
			{
				{ EndPoints.Create, string.Format(Config.Items.ControllerPath, Config.ApiVersion, Config.CreateKeyword, Config.Items.Endpoints.Create) },
				{ EndPoints.Get, string.Format(Config.Items.ControllerPath, Config.ApiVersion, Config.GetKeyword, Config.Items.Endpoints.Get) },
				{ EndPoints.GetAll, string.Format(Config.Items.ControllerPath, Config.ApiVersion, Config.GetAllKeyword, Config.Items.Endpoints.GetAll) },
				{ EndPoints.Update, string.Format(Config.Items.ControllerPath, Config.ApiVersion, Config.UpdateKeyword, Config.Items.Endpoints.Update) },
				{ EndPoints.Delete, string.Format(Config.Items.ControllerPath, Config.ApiVersion, Config.DeleteKeyword, Config.Items.Endpoints.Delete) }
			};

			PlayerEndpoints = new Dictionary<EndPoints, string>
			{
				{ EndPoints.Create, string.Format(Config.Player.ControllerPath, Config.ApiVersion, Config.CreateKeyword, Config.Player.Endpoints.Create) },
				{ EndPoints.Get, string.Format(Config.Player.ControllerPath, Config.ApiVersion, Config.CreateKeyword, Config.Player.Endpoints.Get) },
				{ EndPoints.GetAll, string.Format(Config.Player.ControllerPath, Config.ApiVersion, Config.CreateKeyword, Config.Player.Endpoints.GetAll) },
				{ EndPoints.Update, string.Format(Config.Player.ControllerPath, Config.ApiVersion, Config.CreateKeyword, Config.Player.Endpoints.Update) },
				{ EndPoints.Delete, string.Format(Config.Player.ControllerPath, Config.ApiVersion, Config.CreateKeyword, Config.Player.Endpoints.Delete) }
			};
		}
	}
}