using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryProject.Helpers;

namespace InventoryProject.Models;

public class Player
{
	/// <summary>
	/// This converts the naming convention here to the naming convention of the table in the json object
	/// </summary>
	[Key]
	[Column(HelperVariables.PlayerId)]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int PlayerId { get; set; }
	public string PlayerName { get; set; } = "";
}