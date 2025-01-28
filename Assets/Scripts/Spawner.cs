using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : PoolableObject
{
    [SerializeField] private T _prefab;

    private ObjectPool<PoolableObject> _pool;
    private int _poolCapacity = 30;
    private int _poolMaxSize = 100;
    private int _numberOfSpawnedObjects = 0;

    public event Action<int, int, int> ObjectsCountChanged;

    protected void InitObjectPool()
    {
        _pool = new ObjectPool<PoolableObject>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (obj) => EnableObj(obj),
            actionOnRelease: (obj) => DisableObj(obj),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);

        UpdateObjectsCountInfo();
    }

    protected virtual void EnableObj(PoolableObject obj)
    {
        obj.gameObject.SetActive(true);

        obj.LifetimeEnded += ReleaseObj;
    }

    protected void DisableObj(PoolableObject obj)
    {
        obj.gameObject.SetActive(false);

        obj.LifetimeEnded -= ReleaseObj;
    }

    protected void GetObj()
    {        
        _pool?.Get();

        _numberOfSpawnedObjects++;

        UpdateObjectsCountInfo();
    }

    protected virtual void ReleaseObj(PoolableObject obj)
    {
        _pool.Release(obj);

        UpdateObjectsCountInfo();
    }

    private void UpdateObjectsCountInfo()
    {
        ObjectsCountChanged?.Invoke(_numberOfSpawnedObjects, _pool.CountAll, _pool.CountActive);
    }
}
