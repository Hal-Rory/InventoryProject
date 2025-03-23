using System.Collections.Generic;
using System.Linq;
using Common.UI;
using Controllers;
using ServerItems;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ItemDisplay : MonoBehaviour
    {
        [SerializeField] private CardPool Cards;
        [SerializeField] private GameObject _displayPanel;
        [SerializeField] private TextMeshProUGUI _displayText;
        private ItemBase _currentItem;
        private void OnEnable()
        {
            GameControllerNetwork.Instance.ItemAPIController.API_GetAll(SetupCards);
            _displayPanel.SetActive(false);
        }

        private void SetupCards(IEnumerable<ItemBase> cards)
        {
            Cards.ReleaseAllCards();
            ItemBase[] items = cards.ToArray();
            foreach (ItemBase item in items)
            {
                ButtonCard bCard = Cards.SpawnedCards.Get().Card as ButtonCard;
                if(!bCard) continue;
                bCard.Set(item.ItemID, item.ItemName);
                bCard.SetAction(() =>
                {
                    _currentItem = item;
                    _displayPanel.SetActive(true);
                    _displayText.SetText(item.ToString());
                });
            }
        }

        public void AddItemToPlayerUI()
        {
            GameControllerNetwork.Instance.Inventory.AddPlayerItem(_currentItem);
        }
    }
}