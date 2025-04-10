using System;

namespace Server
{
	[Serializable]
	public class ApiEndpoints
	{
		public ApiEndpoints(string create, string get, string getAll, string update, string delete)
		{
			Create = create;
			Get = get;
			GetAll = getAll;
			Update = update;
			Delete = delete;
		}

		public string Create {get; set;}
		public string Get {get; set;}
		public string GetAll {get; set;}
		public string Update {get; set;}
		public string UpdateAll {get; set;}
		public string Delete {get; set;}
	}
}