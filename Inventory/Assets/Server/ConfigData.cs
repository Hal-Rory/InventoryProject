using System;

namespace Server
{
	[Serializable]
	public class ConfigData
	{
		public ConfigData(string apiVersion, string createKeyword, string getKeyword, string getAllKeyword, string updateKeyword, string deleteKeyword, ApiRoutes inventory, ApiRoutes items, ApiRoutes player)
		{
			ApiVersion = apiVersion;
			CreateKeyword = createKeyword;
			GetKeyword = getKeyword;
			GetAllKeyword = getAllKeyword;
			UpdateKeyword = updateKeyword;
			DeleteKeyword = deleteKeyword;
			Inventory = inventory;
			Items = items;
			Player = player;
		}

		public string ApiUrl;

		public string ApiVersion {get; set;}

		public string CreateKeyword {get; set;}
		public string GetKeyword {get; set;}
		public string GetAllKeyword {get; set;}
		public string UpdateKeyword {get; set;}
		public string DeleteKeyword {get; set;}

		public ApiRoutes Inventory { get; set; }
		public ApiRoutes Items { get; set; }
		public ApiRoutes Player { get; set; }

		public string Construct(ApiRoutes route, EndPoints endPoints)
		{
			return endPoints switch
			{
				EndPoints.Create => string.Format(route.ControllerPath, ApiVersion, CreateKeyword,
					route.Endpoints.Create),
				EndPoints.Get => string.Format(route.ControllerPath, ApiVersion, GetKeyword, route.Endpoints.Get),
				EndPoints.GetAll => string.Format(route.ControllerPath, ApiVersion, GetAllKeyword,
					route.Endpoints.GetAll),
				EndPoints.Update => string.Format(route.ControllerPath, ApiVersion, UpdateKeyword,
					route.Endpoints.Update),
				EndPoints.Delete => string.Format(route.ControllerPath, ApiVersion, DeleteKeyword,
					route.Endpoints.Delete),
				_ => null
			};
		}
	}
}