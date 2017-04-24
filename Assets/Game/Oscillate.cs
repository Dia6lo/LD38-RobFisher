using UnityEngine;

public class Oscillate : MonoBehaviour
{
    private float start;
    private const float MaxOffset = 0.1f;
    private const float Speed = 0.1f;
    private Vector3 direction = Vector3.up;

    private void Start()
    {
        start = transform.localPosition.y;
        transform.Translate(0, Random.Range(-1, 1) * MaxOffset, 0, Space.World);
        direction = Random.value > 0.5f ? Vector3.up : Vector3.down;
    }

    private void Update()
    {
        transform.Translate(direction * Speed * Time.deltaTime, Space.World);
        if (transform.localPosition.y > start + MaxOffset)
            direction = Vector3.down;
        if (transform.localPosition.y < start - MaxOffset)
            direction = Vector3.up;
    }
}