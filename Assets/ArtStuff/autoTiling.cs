using UnityEngine;

public class autoTiling : MonoBehaviour
{
    public float scrollSpeed = 0.5F;
    new Renderer renderer;
    void Start() {
        renderer = GetComponent<Renderer>();
    }
    void Update() {
        float offset = Time.time * scrollSpeed;
        renderer.material.SetTextureOffset("_MainTex", new Vector2(offset, -0.01f));
    }
}