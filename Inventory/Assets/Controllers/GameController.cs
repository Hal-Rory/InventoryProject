using Server;
using UnityEngine;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        public ItemAPI ItemController;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

    }
}