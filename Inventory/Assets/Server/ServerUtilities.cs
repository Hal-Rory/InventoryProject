using Newtonsoft.Json;

namespace Server
{
	public static class ServerUtilities
	{
		public const string CurrentJsonSchema = "http://json-schema.org/draft-07/schema#";
		public const string JsonSchema = "$schema";
		public const string JsonType = "$type";

		public static readonly JsonSerializerSettings JsonSerializer = new JsonSerializerSettings
		{
			TypeNameHandling = TypeNameHandling.All,
			TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
		};
	}
}