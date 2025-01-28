using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    private Vector3 _nextBombPosition;

    private void Awake()
    {
        InitObjectPool();
    }

    public void Spawn(Vector3 position)
    {
        _nextBombPosition = position;
        GetObj();
    }

    protected override void EnableObj(PoolableObject obj)
    {
        if (obj is Bomb bomb)
        {
            base.EnableObj(obj);

            bomb.Init(_nextBombPosition);
        }
    }
}
