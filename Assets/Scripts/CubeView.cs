using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class CubeView : MonoBehaviour
{
    public void Modify(float scale)
    {
        gameObject.transform.localScale = new(scale, scale, scale);
        GetComponent<Renderer>().material.color = new(Random.value, Random.value, Random.value);
    }
}