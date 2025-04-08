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
        public event Action OnNetworkControllersLoaded;

        public static GameControllerNetwork NetworkInstance { get; private set; }
        protected virtual void Awake()
        {
            if (NetworkInstance == null)
            {
                NetworkInstance = this;
                DontDestroyOnLoad(this);
                ConfigLoader.OnConfigLoaded += LoadControllers;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        protected virtual void LoadControllers()
        {
            ItemAPIController.SetConfig(ConfigLoader);
            PlayerAPIController.SetConfig(ConfigLoader);
            InventoryAPIController.SetConfig(ConfigLoader);
            OnNetworkControllersLoaded?.Invoke();
        }
    }
}