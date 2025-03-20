using System.Collections.Generic;
using System.Linq;
using Common.UI;
using Controllers;
using Server;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour
{
	[Header("Display")]
	[SerializeField] private GameObject _playerInteractiveCover;

	[Header("Player")]
	[SerializeField] private TextMeshProUGUI _playerNameText;
	[SerializeField] private TMP_InputField _playerIdInput;
	private Player _currentPlayer => GameController.Instance.Inventory.CurrentPlayer;

	[Header("Warning")]
	[SerializeField] private GameObject _warningPanel;
	[SerializeField] private TextMeshProUGUI _warningText;

	[Header("Items")]
	[SerializeField] private CardPool Cards;
	[SerializeField] private TextMeshProUGUI _itemDisplayText;
	[SerializeField] private GameObject _itemDisplayPanel;
	private PlayerItem _selectedItem;

	private InventoryController _inventoryController => GameController.Instance.Inventory;

	private void Start()
	{
		_inventoryController.OnPlayerInventoryUpdated += OnPlayerInventoryUpdated;
	}

	private void OnPlayerInventoryUpdated(Player player)
	{
		if (_currentPlayer.PlayerId != player.PlayerId) return;
		if (_currentPlayer.PlayerId == -1) return;
		ShowWarning(string.Empty);
		DeselectItem();
		_playerInteractiveCover.SetActive(false);
		DisplayPlayer(_currentPlayer);
	}

	private void OnEnable()
	{
		Cards.gameObject.SetActive(false);
	}

	private void SelectItem(PlayerItem item)
	{
		_itemDisplayPanel.SetActive(true);
		_itemDisplayText.SetText($"{item.ItemQuantity} {item.Item}");
		_selectedItem = item;
	}

	private void DeselectItem()
	{
		_itemDisplayPanel.SetActive(false);
		_itemDisplayText.SetText(string.Empty);
		_selectedItem = null;
	}

	/// <summary>
	/// User has entered a player ID and pressed the get player button
	/// </summary>
	public void DisplayPlayerUI()
	{
		_playerInteractiveCover.SetActive(true);
		DeselectItem();
		if (!int.TryParse(_playerIdInput.text, out int id))
		{
			CancelPlayer();
			ShowWarning("Enter a valid ID");
			return;
		}
		ShowWarning(string.Empty);
		if (_currentPlayer != null && id == _currentPlayer.PlayerId)
		{
			DisplayPlayer(_currentPlayer);
		}
		else
		{
			GameController.Instance.PlayerAPIController.API_Get(id, DisplayPlayer);
		}
	}

	/// <summary>
	/// User has selected an item and pressed the remove item button
	/// </summary>
	public void RemoveItemUI()
	{
		_inventoryController.RemovePlayerItem(_selectedItem.Item);
	}

	/// <summary>
	/// The player has been selected but the items have not been loaded in
	/// </summary>
	/// <param name="player"></param>
	private void DisplayPlayer(Player player)
	{
		_inventoryController.SetCurrentPlayer(player);
		_playerNameText.text = _currentPlayer.PlayerName;
		GameController.Instance.InventoryAPIController.API_GetAll(player.PlayerId, SetupCards);
	}

	/// <summary>
	/// The items have been loaded in so populate the display containing that will contain them
	/// by setting up the new round of cards from the pool
	/// </summary>
	/// <param name="cards"></param>
	private void SetupCards(IEnumerable<PlayerItem> cards)
	{
		Cards.ReleaseAllCards();
		PlayerItem[] items = cards.ToArray();
		Cards.gameObject.SetActive(true);
		_itemDisplayText.text = "Select an item";

		if (_currentPlayer.Items == null) _currentPlayer.Items = new HashSet<string>();
		else _currentPlayer.Items.Clear();

		foreach (PlayerItem item in items)
		{
			ButtonCard bCard = Cards.SpawnedCards.Get().Card as ButtonCard;
			if (!bCard) continue;
			bCard.Set(item.Item.ItemID, item.Item.ItemName);
			bCard.SetAction(() =>
			{
				SelectItem(item);
			});
			_currentPlayer.Items.Add(item.Item.ItemID);
		}
		_playerInteractiveCover.SetActive(false);
	}

	private void ShowWarning(string warning)
	{
		_warningText.text = warning;
		_warningPanel.SetActive(!string.IsNullOrEmpty(warning));
	}

	private void CancelPlayer()
	{
		_playerInteractiveCover.SetActive(false);
		_itemDisplayPanel.SetActive(false);
		Cards.gameObject.SetActive(false);
		_inventoryController.RemoveCurrentPlayer();
		_playerNameText.text = string.Empty;
	}
}