using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Explosion))]
public class Cube : MonoBehaviour
{
    [SerializeField] private CubeView _cubeView;
    [SerializeField] private Explosion _explosion;
    [SerializeField] private float _decayProbability = 1f;
    [SerializeField] private float _robabilityMultiplier = 0.5f;
    [SerializeField] private float _scaleMultiplier = 0.5f;

    public event Action<Cube, float, float> Separation;
    public event Action<Cube> Destroyed;

    private void OnMouseUpAsButton()
    {
        if (_decayProbability > UnityEngine.Random.value)
            Separation?.Invoke(this, transform.localScale.x, _decayProbability);

        Destroyed?.Invoke(this);
        Destroy(gameObject);
    }

    public void Initialize(float scale, float decayProbability)
    {
        _decayProbability = decayProbability * _robabilityMultiplier;
        _cubeView.Modify(scale * _scaleMultiplier);
    }

    public void Explode(List<Rigidbody> rigidbodies)
    {
        _explosion.Explode(rigidbodies);
    }
}