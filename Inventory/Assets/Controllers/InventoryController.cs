using System;
using Server;
using ServerItems;

namespace Controllers
{
    public class InventoryController
    {
        public event Action<Player> OnPlayerInventoryUpdated;
        public Player CurrentPlayer { get; private set; }
        private bool _busy;

        public void SetCurrentPlayer(Player playerId)
        {
            CurrentPlayer = playerId;
        }

        public void RemoveCurrentPlayer()
        {
            CurrentPlayer = null;
        }

        /// <summary>
        /// If the request to add the item was valid
        /// </summary>
        /// <param name="item"></param>
        /// <param name="quantity"></param>
        /// <returns>True if the item is going to be added(not if the update succeeded)</returns>
        public void AddPlayerItem(ItemBase item, int quantity = 1)
        {
            if (CurrentPlayer == null || CurrentPlayer.PlayerId < 0 || _busy) return;
            _busy = true;
            if (CurrentPlayer.Items.Contains(item.ItemID))
            {
                GameController.Instance.InventoryAPIController.API_Update(new InventoryItem
                {
                    Item = item.ItemID,
                    ItemQuantity = quantity,
                    Player = CurrentPlayer.PlayerId
                }, success =>
                {
                    if (success) OnPlayerInventoryUpdated?.Invoke(CurrentPlayer);
                    _busy = false;
                });
            }
            else
            {
                GameController.Instance.InventoryAPIController.API_Create(new InventoryItem
                {
                    Item = item.ItemID,
                    ItemQuantity = quantity,
                    Player = CurrentPlayer.PlayerId
                }, success =>
                {
                    if (success) OnPlayerInventoryUpdated?.Invoke(CurrentPlayer);
                    _busy = false;
                });
            }
        }

        //use update and let db handle "removal" and leave this as a function that actually just deletes the entire object
        public void RemovePlayerItem(ItemBase itemId)
        {
            if (CurrentPlayer == null || CurrentPlayer.PlayerId < 0 || _busy) return;
            _busy = true;
            GameController.Instance.InventoryAPIController.API_Delete(CurrentPlayer.PlayerId, itemId.ItemID,
                success =>
                {
                    if (success) OnPlayerInventoryUpdated?.Invoke(CurrentPlayer);
                    _busy = false;
                });
        }
    }
}