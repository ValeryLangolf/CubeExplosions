using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    public void Explode(List<Rigidbody> explodableCubes)
    {
        foreach (Rigidbody explodableCube in explodableCubes)
        {
            explodableCube.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }
}