using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int _minimumObjects = 2;
    [SerializeField] private int _maximumObjects = 6;

    private List<Cube> _cubes;

    private void Awake()
    {
        _cubes = GetComponentsInChildren<Cube>().ToList();
    }

    private void OnEnable()
    {
        foreach (Cube cube in _cubes)
        {
            Subscribe(cube);
        }
    }

    private void OnDisable()
    {
        foreach (Cube cube in _cubes)
        {
            Unsubscribe(cube);
        }
    }

    private void Subscribe(Cube cube)
    {
        cube.Separation += CreateCubes;
        cube.Destroyed += RemoveCube;
    }

    private void Unsubscribe(Cube cube)
    {
        cube.Separation -= CreateCubes;
        cube.Destroyed -= RemoveCube;
    }

    private void CreateCubes(Cube cubeSource, float scale, float decayProbability)
    {
        int maximumObjects = _maximumObjects + 1;
        int countNewObjects = Random.Range(_minimumObjects, maximumObjects);

        List<Rigidbody> rigidbodies = new();

        for (int i = 0; i < countNewObjects; i++)
        {
            Cube newCube = Create(cubeSource.gameObject);
            _cubes.Add(newCube);
            Subscribe(newCube);

            rigidbodies.Add(newCube.GetComponent<Rigidbody>());
            newCube.Initialize(scale, decayProbability);
        }

        cubeSource.Explode(rigidbodies);
    }

    private void RemoveCube(Cube cube)
    {
        Unsubscribe(cube);
        _cubes?.Remove(cube);
    }

    private Cube Create(GameObject cubeSource)
    {
        GameObject gameObject = Instantiate(cubeSource, cubeSource.transform.position, Quaternion.identity, transform);
        gameObject.name = cubeSource.name;

        return gameObject.GetComponent<Cube>();
    }
}