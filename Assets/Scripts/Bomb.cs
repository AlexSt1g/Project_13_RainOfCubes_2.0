using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class Bomb : PoolableObject
{
    private int _minLifetime = 2;
    private int _maxLifetime = 5;
    private Rigidbody _rigidbody;
    private Renderer _renderer;
    private float _defalutMaterialAlphaValue;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        _defalutMaterialAlphaValue = _renderer.material.color.a;
    }

    public void Init(Vector3 position)
    {
        transform.position = position;
        _rigidbody.velocity = Vector3.zero;

        StartCoroutine(BecomeTransparentWithLifetime(GetLifetime(_minLifetime, _maxLifetime)));
    }

    private IEnumerator BecomeTransparentWithLifetime(int lifetime)
    {
        for (float i = lifetime; i > 0; i -= Time.deltaTime)
        {
            yield return null;

            ChangeMaterialAlphaValue(_defalutMaterialAlphaValue * GetTransparencyPercent(i, lifetime));
        }

        ChangeMaterialAlphaValue(_defalutMaterialAlphaValue);

        EndLifetime();
    }

    private void ChangeMaterialAlphaValue(float alphaValue)
    {
        Color tmpColor = _renderer.material.color;
        tmpColor.a = alphaValue;
        _renderer.material.color = tmpColor;
    }

    private float GetTransparencyPercent(float currentTime, float lifetime)
    {
        return currentTime / lifetime;
    }
}
