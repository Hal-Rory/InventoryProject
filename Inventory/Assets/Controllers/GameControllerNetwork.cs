using System;
using Server;
using UnityEngine;

namespace Controllers
{
    public class GameControllerNetwork : MonoBehaviour
    {
        public ItemAPI ItemAPIController;
        public PlayerAPI PlayerAPIController;
        public InventoryAPI InventoryAPIController;
        public ConfigLoader ConfigLoader;
        public event Action OnControllersLoaded;

        public static GameControllerNetwork NetworkInstance { get; private set; }
        private void Awake()
        {
            if (NetworkInstance == null)
            {
                NetworkInstance = this;
                DontDestroyOnLoad(this);
                ConfigLoader.OnConfigLoaded += LoadScene;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void LoadScene()
        {
            ItemAPIController.SetConfig(ConfigLoader);
            PlayerAPIController.SetConfig(ConfigLoader);
            InventoryAPIController.SetConfig(ConfigLoader);
            OnControllersLoaded?.Invoke();
        }
    }
}