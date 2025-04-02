using Server;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class GameControllerNetwork : MonoBehaviour
    {

        public ItemAPI ItemAPIController;
        public PlayerAPI PlayerAPIController;
        public InventoryAPI InventoryAPIController;
        public InventoryController Inventory;
        public ConfigLoader ConfigLoader;

        public static GameControllerNetwork Instance { get; private set; }
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Inventory = new InventoryController();
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
            SceneManager.LoadScene(1);
        }
    }
}