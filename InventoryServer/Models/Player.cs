using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryProject.Helpers;

namespace InventoryProject.Models;

public class Player
{
	[Key]
	[Column(HelperVariables.PlayerId)]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int PlayerId { get; set; }
	public string PlayerName { get; set; } = "";
}