using Common.UI;
using UnityEngine;
using UnityEngine.Pool;

public class PooledCard : MonoBehaviour
{
    private bool _isActive;
    private ObjectPool<PooledCard> _pool;
    private Transform _parent;
    public Card Card { get; private set; }

    public void Activate(Transform parent, Card card, ObjectPool<PooledCard> pool)
    {
        _isActive = true;
        Card = card;
        _parent = parent;
        _pool = pool;
        Card.gameObject.SetActive(false);
        Card.transform.SetParent(_parent);
        Card.transform.localPosition = Vector3.zero;
    }

    public void Get()
    {
        Card.gameObject.SetActive(true);
        Card.transform.SetParent(_parent);
        _isActive = true;
    }

    public void Release()
    {
        if (!_isActive) return;
        _isActive = false;
        _pool.Release(this);
        Card.gameObject.SetActive(false);
        Card.transform.SetParent(null);
    }
}