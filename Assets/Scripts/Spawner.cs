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
        cube.Separation += CreateList;
        cube.Destroyed += Remove;
    }

    private void Unsubscribe(Cube cube)
    {
        cube.Separation -= CreateList;
        cube.Destroyed -= Remove;
    }

    private void CreateList(Cube cubeToCreate)
    {
        int maximumObjects = _maximumObjects + 1;
        int countNewObjects = Random.Range(_minimumObjects, maximumObjects);

        List<Rigidbody> rigidbodies = new List<Rigidbody>();

        for (int i = 0; i < countNewObjects; i++)
        {
            Cube cube = Create(cubeToCreate.gameObject);
            _cubes.Add(cube);
            Subscribe(cube);

            cubeToCreate.Initialize(cube);
            rigidbodies.Add(cube.GetComponent<Rigidbody>());
        }

        cubeToCreate.Explode(rigidbodies);
    }

    private void Remove(Cube cube)
    {
        Unsubscribe(cube);
        _cubes?.Remove(cube);
    }

    private Cube Create(GameObject objectToCreate)
    {
        GameObject gameObject = Instantiate(objectToCreate, objectToCreate.transform.position, Quaternion.identity, transform);
        gameObject.name = objectToCreate.name;

        return gameObject.GetComponent<Cube>();
    }
}