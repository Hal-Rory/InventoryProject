using InventoryProject.Databases;
using InventoryProject.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryProject.Services;

public class PlayerService
{
	private readonly InventoryDbContext _context;

	public PlayerService(InventoryDbContext context)
	{
		_context = context;
	}

	public async Task<PlayerBase?> GetPlayer(int playerId)
	{
		return await _context.Player.FindAsync(playerId);
	}

	public async Task<(List<PlayerBase>?, string)> GetAllPlayers()
	{
		try
		{
			List<PlayerBase> players = await _context.Player.ToListAsync();
			return (players, string.Empty);
		}
		catch (Exception e)
		{
			return (null, $"An error occurred while retrieving players. {e.Message}" );
		}
	}

	/// <summary>
	/// Try creating a player
	/// </summary>
	/// <param name="player"></param>
	/// <returns>a response from the creation process, will return null on success</returns>
	public async Task<string?> CreatePlayer(PlayerBase player)
	{
		try
		{
			_context.Player.Add(player);
			int result = await _context.SaveChangesAsync();
			if (result <= 0)
			{
				_context.Player.Remove(player);
				return $"Player could not be added.";
			}
		}
		catch (DbUpdateException dbue)
		{
			return $"Player could not be added.\n{dbue.Message}";
		}
		return null;
	}
}