using UnityEngine;

public class Modifier : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private float _scaleMultiplier = 0.5f;

    private void OnEnable()
    {
        _spawner.GameObjectCreated += Modify;
    }

    private void OnDisable()
    {
        _spawner.GameObjectCreated -= Modify;
    }

    private void Modify(GameObject gameObject)
    {
        gameObject.transform.localScale *= _scaleMultiplier;

        if (gameObject.TryGetComponent<Renderer>(out Renderer renderer))
        {
            Color randomColor = new(Random.value, Random.value, Random.value);
            renderer.material.color = randomColor;
        }
        else
        {
            Debug.LogWarning("У объекта нет компонента Renderer :(");
        }
    }
}