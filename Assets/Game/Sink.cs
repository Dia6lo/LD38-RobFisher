using UnityEngine;

public class Sink : MonoBehaviour
{
    public float Speed;

    private void Update()
    {
        transform.Translate(Vector3.down * Speed * Time.deltaTime);
    }
}