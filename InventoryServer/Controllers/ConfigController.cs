using InventoryProject.Configuration;
using InventoryProject.Helpers;
using InventoryProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.IO.File;

namespace InventoryProject.Controllers;

[ApiController]
[Route("[controller]")]
public class ConfigController : ControllerBase
{
	private readonly string _configFilePath;

	public ConfigController(IOptions<ConfigSettings> configSettings)
	{
		_configFilePath = configSettings.Value.ConfigFilePath;
	}

	[HttpGet("Get/Config")]
	public async Task<IActionResult> GetConfig()
	{
		if (!Exists(_configFilePath))
		{
			return NotFound(new { message = "Configuration file not found" });
		}

		string json = await ReadAllTextAsync(_configFilePath);
		ConfigData? config = JsonConvert.DeserializeObject<ConfigData>(json);
		//consider making the clients have to fill this in on their end for extra protection
		if(config != null) config.ApiVersion = HelperVariables.SwaggerVersion;
		return Ok(config);
	}
}