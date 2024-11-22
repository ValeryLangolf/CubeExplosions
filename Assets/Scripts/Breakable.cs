using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _decayProbability = 1f;
    [SerializeField] private float _valueIncreaseChance = 0.5f;


    private void OnMouseUpAsButton()
    {
        if(_decayProbability > Random.value)
        {
            List<GameObject> gameObjects = _spawner.CreateList(gameObject);
            ReduceChanceExplosionInObjects(gameObjects);
            Explode(gameObjects);
        }
        
        Destroy(gameObject);
    }

    public void SetDecayProbability(float chance)
    {
        _decayProbability = chance;
    }

    private void Explode(List<GameObject> explodableObjects)
    {
        foreach (GameObject explodableObject in explodableObjects)
        {
            explodableObject.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }

    private void ReduceChanceExplosionInObjects(List<GameObject> explodableObjects)
    {
        foreach (GameObject explodableObject in explodableObjects)
        {
            float chance = _decayProbability * _valueIncreaseChance;
            explodableObject.GetComponent<Breakable>().SetDecayProbability(chance);
        }
    }
}