namespace InventoryProject.Models
{
	[Serializable]
	public class ApiRoutes
	{
		public ApiRoutes(string controllerPath, ApiEndpoints endpoints)
		{
			ControllerPath = controllerPath;
			Endpoints = endpoints;
		}

		public string ControllerPath { get; set; }
		public ApiEndpoints Endpoints { get; set; }
	}
}