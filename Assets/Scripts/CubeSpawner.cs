using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private Transform _floor;    
    [SerializeField] private float _startPointHeight = 50f;
    [SerializeField] private float _repeatRate = 0.2f;
    [SerializeField] private BombSpawner _bombSpawner;

    private Vector3 _startPoint;
    private WaitForSeconds _waitRepeatRate;    

    protected override void OnAwake()
    {
        _startPoint = _floor.transform.position;
        _startPoint.y += _startPointHeight;
        _waitRepeatRate = new WaitForSeconds(_repeatRate);
    }

    private void Start()
    {         
        StartCoroutine(RepeatGetCube());
    }

    protected override void EnableObj(PoolableObject cube)
    {
        SetSpawnPoint(cube);

        base.EnableObj(cube);
    }

    protected override void ReleaseObj(PoolableObject cube)
    {
        base.ReleaseObj(cube);

        _bombSpawner.Spawn(cube.transform.position);
    }

    private IEnumerator RepeatGetCube()
    {
        while (enabled)
        {
            GetObj();
            yield return _waitRepeatRate;
        }
    }

    private void SetSpawnPoint(PoolableObject cube)
    {
        int halfOfScaleCoefficient = 2;
        int planeScaleCoefficient = 10;

        float sidestepX = _floor.transform.localScale.x / halfOfScaleCoefficient * planeScaleCoefficient;
        float sidestepZ = _floor.transform.localScale.z / halfOfScaleCoefficient * planeScaleCoefficient;

        cube.transform.position = new Vector3(Random.Range(-sidestepX, sidestepX), _startPoint.y, Random.Range(-sidestepZ, sidestepZ));
    }
}
