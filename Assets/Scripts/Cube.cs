using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CubeView))]
[RequireComponent(typeof(Explosion))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _decayProbability = 1f;
    [SerializeField] private float _valueIncreaseChance = 0.5f;

    public event Action<Cube> Separation;
    public event Action<Cube> Destroyed;

    private Cube _cube;

    private void Awake()
    {
        _cube = GetComponent<Cube>();
    }

    private void OnMouseUpAsButton()
    {
        if (_decayProbability > UnityEngine.Random.value)
            Separation?.Invoke(_cube);

        Destroyed?.Invoke(gameObject.GetComponent<Cube>());
        Destroy(gameObject);
    }

    public void Initialize(Cube cube)
    {
        ReduceChanceExplosion(cube);
        cube.GetComponent<CubeView>().Modify();
    }

    public void Explode(List<Rigidbody> rigidbodies)
    {
        gameObject.GetComponent<Explosion>().Explode(rigidbodies);
    }

    public void SetDecayProbability(float chance)
    {
        _decayProbability = chance;
    }

    private void ReduceChanceExplosion(Cube cube)
    {
        float chance = _decayProbability * _valueIncreaseChance;
        cube.SetDecayProbability(chance);
    }
}