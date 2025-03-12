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

	public PlayerController(PlayerService playerService)
	{
		_PlayerService = playerService;
	}

	[HttpPost("Create/Player")]
	public async Task<IActionResult> CreatePlayer(string playerName)
	{
		bool success = await _PlayerService.CreatePlayer(new PlayerBase{PlayerName = playerName});
		return Ok($"Player {(success ? "" : "not ")}created");
	}

	[HttpGet("Get-All/Players")]
	public async Task<ActionResult<List<PlayerBase>>> GetAllPlayers()
	{
		List<PlayerBase> players = await _PlayerService.GetAllPlayers();
		return Ok(players);
	}

	[HttpGet("Get/Player")]
	public async Task<ActionResult<PlayerBase>> GetPlayer(int id)
	{
		PlayerBase? player = await _PlayerService.GetPlayer(id);
		if (player == null)
		{
			return NotFound();
		}
		return Ok(player);
	}

	[HttpDelete("Remove/Player")]
	public async Task<IActionResult> DeletePlayer(int playerId)
	{
		bool success = await _PlayerService.RemovePlayer(playerId);
		return Ok($"Player{(success ? " " : "not ")}removed");
	}
}