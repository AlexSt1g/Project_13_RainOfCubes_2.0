using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    private Vector3 _nextBombPosition;    

    public void Spawn(Vector3 position)
    {
        _nextBombPosition = position;
        GetObj();
    }

    protected override void EnableObj(PoolableObject obj)
    {
        Bomb bomb = (Bomb)obj;

        base.EnableObj(bomb);

        bomb.Init(_nextBombPosition);
    }
}
