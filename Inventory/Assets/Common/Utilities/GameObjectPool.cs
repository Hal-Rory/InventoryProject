using System.Collections.Generic;
using UnityEngine;

namespace Common.Utilities
{
    public class GameObjectPool : MonoBehaviour
    {
        [field: SerializeField] public Transform Parent { get; private set;}
        private Dictionary<string, GameObject> _spawnedCards = new Dictionary<string, GameObject>();
        public int PoolObjectCount => _spawnedCards.Count;

        public T SpawnItem<T>(string id, GameObject prefab)
            where T: Component
        {
            T spawnedObject = Instantiate(prefab, Parent).GetComponent<T>();
            _spawnedCards.Add(id, spawnedObject.gameObject);
            return spawnedObject;
        }

        public bool TrySpawnItem<T>(string id, GameObject prefab, out T component)
            where T: Component
        {
            component = null;
            if (_spawnedCards.ContainsKey(id)) return false;
            T spawnedObject = Instantiate(prefab, Parent).GetComponent<T>();
            if (!_spawnedCards.TryAdd(id, spawnedObject.gameObject))
            {
                Destroy(spawnedObject.gameObject);
                return false;
            }
            component = spawnedObject;
            return true;
        }

        public GameObject GetObject(string id)
        {
            return _spawnedCards[id];
        }

        public void ClearObjects()
        {
            if (_spawnedCards == null) return;
            int count = _spawnedCards.Count;
            if (count == 0) return;
            foreach (GameObject obj in _spawnedCards.Values)
            {
                Destroy(obj);
            }
            _spawnedCards.Clear();
        }
    }
}