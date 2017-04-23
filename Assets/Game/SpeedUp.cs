using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    private void Update()
    {
        Time.timeScale = Input.GetKey(KeyCode.LeftShift) ? 4 : 1;
    }
}