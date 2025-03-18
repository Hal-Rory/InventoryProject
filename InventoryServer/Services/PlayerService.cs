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

	public async Task<bool> CreatePlayer(Player player)
	{
		_context.Player.Add(player);
		int result = await _context.SaveChangesAsync();
		return result > 0;
	}

	public async Task<Player?> GetPlayer(int playerId)
	{
		Player? player = await _context.Player.FindAsync(playerId);
		return player ?? throw new KeyNotFoundException($"No player with id: {playerId} was found");
	}

	public async Task<List<Player>> GetAllPlayers()
	{
		List<Player> players = await _context.Player.ToListAsync();
		return players;
	}

	public async Task<bool> RemovePlayer(int playerId)
	{
		Player? player = await _context.Player.FindAsync(playerId);
		if (player == null) throw new KeyNotFoundException($"No player with id: {playerId} was found");
		_context.Player.Remove(player);
		int result = await _context.SaveChangesAsync();
		return result > 0;
	}
}