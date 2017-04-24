using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    public float Speed = 4;

    private void Update()
    {
        Time.timeScale = Input.GetButton("SpeedUp") ? Speed : 1;
    }
}