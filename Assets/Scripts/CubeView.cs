using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class CubeView : MonoBehaviour
{
    [SerializeField] private float _scaleMultiplier = 0.5f;

    public void Modify()
    {
        gameObject.transform.localScale *= _scaleMultiplier;

        Color randomColor = new(Random.value, Random.value, Random.value);
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = randomColor;
    }
}