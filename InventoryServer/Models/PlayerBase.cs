using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryProject.Helpers;

namespace InventoryProject.Models;

public class PlayerBase
{
	[Key]
	[Column(HelperVariables.PlayerId)]
	public virtual string PlayerId { get; set; }
	public string PlayerName { get; set; }
}