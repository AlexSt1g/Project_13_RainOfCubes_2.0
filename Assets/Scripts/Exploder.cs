using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private PoolableObject _obj;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    private void OnEnable()
    {
        _obj.LifetimeEnded += Explode;
    }

    private void OnDisable()
    {
        _obj.LifetimeEnded -= Explode;
    }

    private void Explode(PoolableObject obj)
    {
        Vector3 explosionPosition = obj.transform.position;

        foreach (Rigidbody explodableObject in GetExplodableObjects(explosionPosition))        
            explodableObject.AddExplosionForce(_explosionForce, explosionPosition, _explosionRadius);        
    }

    private List<Rigidbody> GetExplodableObjects(Vector3 explosionPosition)
    {
        List<Rigidbody> explodableObjects = new();

        Collider[] colliders = Physics.OverlapSphere(explosionPosition, _explosionRadius);
        
        foreach (Collider hit in colliders)
            if (hit.attachedRigidbody != null)
                explodableObjects.Add(hit.attachedRigidbody);
            
        return explodableObjects;
    }
}
