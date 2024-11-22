using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CubeView))]
[RequireComponent(typeof(Explosion))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _decayProbability = 1f;
    [SerializeField] private float _robabilityMultiplier = 0.5f;
    [SerializeField] private float _scaleMultiplier = 0.5f;

    public event Action<Cube, float, float> Separation;
    public event Action<Cube> Destroyed;

    private void OnMouseUpAsButton()
    {
        Cube cube = GetComponent<Cube>();

        if (_decayProbability > UnityEngine.Random.value)
            Separation?.Invoke(cube, transform.localScale.x, _decayProbability);

        Destroyed?.Invoke(cube);
        Destroy(gameObject);
    }

    public void Initialize(float scale, float decayProbability)
    {
        _decayProbability = decayProbability * _robabilityMultiplier;
        GetComponent<CubeView>().Modify(scale * _scaleMultiplier);
    }

    public void Explode(List<Rigidbody> rigidbodies)
    {
        GetComponent<Explosion>().Explode(rigidbodies);
    }
}