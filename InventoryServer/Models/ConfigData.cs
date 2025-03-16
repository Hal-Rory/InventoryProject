namespace InventoryProject.Models
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

		public string ApiVersion {get; set;}

		public string CreateKeyword {get; set;}
		public string GetKeyword {get; set;}
		public string GetAllKeyword {get; set;}
		public string UpdateKeyword {get; set;}
		public string DeleteKeyword {get; set;}

		public ApiRoutes Inventory { get; set; }
		public ApiRoutes Items { get; set; }
		public ApiRoutes Player { get; set; }
	}
}