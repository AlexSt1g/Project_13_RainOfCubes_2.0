using TMPro;
using UnityEngine;

public class SpawnerInfo<T> : MonoBehaviour where T : PoolableObject
{
    [SerializeField] private Spawner<T> _spawner;
    [SerializeField] private TextMeshProUGUI _numberOfSpawnedObjects;
    [SerializeField] private TextMeshProUGUI _numberOfAllObjects;
    [SerializeField] private TextMeshProUGUI _numberOfActiveObjects;

    private void OnEnable()
    {
        _spawner.ObjectsCountChanged += ShowSpawnerObjectsInfo;
    }

    private void OnDisable()
    {
        _spawner.ObjectsCountChanged -= ShowSpawnerObjectsInfo;
    }

    private void ShowSpawnerObjectsInfo(int numberOfSpawnedObjects, int numberOfAllObjects, int numberOfActiveObjects)
    {        
        _numberOfSpawnedObjects.text = numberOfSpawnedObjects.ToString();
        _numberOfAllObjects.text = numberOfAllObjects.ToString();
        _numberOfActiveObjects.text = numberOfActiveObjects.ToString();
    }
}
