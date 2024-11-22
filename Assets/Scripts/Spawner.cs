using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private const int MinimumObjects = 2;
    private const int MaximumObjects = 6;

    public event Action<GameObject> GameObjectCreated;

    public List<GameObject> CreateList(GameObject objectToCreate)
    {
        int countNewObjects = UnityEngine.Random.Range(MinimumObjects, MaximumObjects);
        List<GameObject> gameObjects = new();

        for (int i = 0; i < countNewObjects; i++)
        {
            GameObject gameObject = Create(objectToCreate);
            gameObjects.Add(gameObject);
            GameObjectCreated?.Invoke(gameObject);
        }

        return gameObjects;
    }

    private GameObject Create(GameObject objectToCreate)
    {
        GameObject gameObject = Instantiate(objectToCreate, objectToCreate.transform.position, Quaternion.identity, transform);
        gameObject.name = objectToCreate.name;

        return gameObject;
    }
}