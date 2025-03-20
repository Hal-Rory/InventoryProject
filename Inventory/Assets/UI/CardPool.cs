using System;
using Common.UI;
using UnityEngine;
using UnityEngine.Pool;

namespace UI
{
    public class CardPool : MonoBehaviour
    {
        public ObjectPool<PooledCard> SpawnedCards;
        //TODO: if this is ever needed on multiple threads, implement this
        private object _poolLock;
        [SerializeField] private Card _cardPrefab;
        [SerializeField] private int _startingPoolSize;
        public Transform ParentTransform;
        private Action _releaseAll;

        private void Awake()
        {
            SpawnedCards = new ObjectPool<PooledCard>(
                CreateCard,
                GetCard,
                ReleaseCard,
                DestroyCard,
                true,
                _startingPoolSize
            );
        }

        public void ReleaseAllCards()
        {
            _releaseAll?.Invoke();
        }

        public void DestroyAllCards()
        {
            SpawnedCards.Clear();
        }
        /// <summary>
        /// Create items and place them into the action to be released when
        /// releaseAll is raised
        /// </summary>
        /// <returns></returns>
        private PooledCard CreateCard()
        {
            Card card = Instantiate(_cardPrefab, ParentTransform);
            PooledCard pooled = card.GetComponent<PooledCard>();
            pooled.Activate(ParentTransform, card, SpawnedCards);
            _releaseAll += pooled.Release;
            return pooled;
        }

        private void GetCard(PooledCard card)
        {
            card.Get();
        }

        private void ReleaseCard(PooledCard pooled)
        {
            if (!pooled) return;
            pooled.gameObject.SetActive(false);
            pooled.transform.SetParent(null);
        }

        private void DestroyCard(PooledCard pooled)
        {
            if(pooled) Destroy(pooled.gameObject);
        }
    }
}