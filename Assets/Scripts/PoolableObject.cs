using UnityEngine;
using UnityEngine.Events;

public class PoolableObject : MonoBehaviour
{
    [SerializeField] private float _minHeightForLiving = -10f;

    public event UnityAction<PoolableObject> LifetimeEnded;

    private void Update()
    {
        if (transform.position.y < _minHeightForLiving)
            EndLifetime();
    }

    protected void EndLifetime()
    {
        LifetimeEnded?.Invoke(this);
    }

    protected int GetLifetime(int minLifeTime, int maxLifeTime)
    {
        return Random.Range(minLifeTime, maxLifeTime + 1);
    }
}
