using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    public float Speed = 4;

    private void Update()
    {
        Time.timeScale = Input.GetKey(KeyCode.LeftShift) ? Speed : 1;
    }
}