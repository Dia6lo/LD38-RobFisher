using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float LifeSpan = 20;
    private float lifeTime;

    private void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime > LifeSpan)
            Destroy(gameObject);
    }
}