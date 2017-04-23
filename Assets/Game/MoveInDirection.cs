using UnityEngine;

public class MoveInDirection : MonoBehaviour
{
    public float Speed;
    public Vector2 Direction;

    private void Update()
    {
        transform.Translate(Direction * Speed * Time.deltaTime);
    }
}