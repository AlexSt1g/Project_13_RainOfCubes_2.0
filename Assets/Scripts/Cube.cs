using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : PoolableObject
{
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _triggeredMaterial;    

    private int _minLifetime = 2;
    private int _maxLifetime = 5;
    private bool _hasBeenCollided;
    private Renderer _renderer;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _renderer.material = _defaultMaterial;
        _rigidbody.velocity = Vector3.zero;
    }

    private void OnDisable()
    {
        _hasBeenCollided = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasBeenCollided == false)
        {
            if (collision.gameObject.GetComponent<Platform>())
            {
                _renderer.material = _triggeredMaterial;

                StartCoroutine(WaitLifetime(GetLifetime(_minLifetime, _maxLifetime)));

                _hasBeenCollided = true;
            }
        }
    }

    private IEnumerator WaitLifetime(int lifetime)
    {
        yield return new WaitForSeconds(lifetime);

        EndLifetime();
    }
}
