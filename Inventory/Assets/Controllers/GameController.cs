using Server;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        public ItemAPI ItemAPIController;
        public PlayerAPI PlayerAPIController;
        public InventoryAPI InventoryAPIController;
        public InventoryController Inventory;
        public ConfigLoader ConfigLoader;

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
            SceneManager.LoadScene(1);
        }
    }
}