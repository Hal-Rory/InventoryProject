using InventoryProject.Helpers;
using InventoryProject.Models;
using InventoryProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryProject.Controllers;
[ApiController]
[Route($"[controller]/{HelperVariables.SwaggerVersion}")]
public class PlayerController : ControllerBase
{
	private readonly PlayerService _PlayerService;

	public PlayerController(PlayerService PlayerService)
	{
		_PlayerService = PlayerService;
	}

	[HttpGet("Get All Players")]
	public async Task<ActionResult<List<PlayerBase>>> GetAllPlayers()
	{
		var (players, error) = await _PlayerService.GetAllPlayers();

		if (players == null)
		{
			return BadRequest(new { message = error });
		}

		return Ok(players);
	}

	[HttpGet("Get Player")]
	public async Task<ActionResult<PlayerBase>> GetPlayer(int id)
	{
		PlayerBase? player = await _PlayerService.GetPlayer(id);
		if (player == null)
		{
			return NotFound();
		}
		return Ok(player);
	}

	[HttpPost("Create Player")]
	public async Task<IActionResult> CreatePlayer(PlayerVm player)
	{
		string? response = await _PlayerService.CreatePlayer(new PlayerBase{PlayerName = player.PlayerName});
		if(!string.IsNullOrEmpty(response)) return Conflict(response);
		return Ok("Player created");
	}
}