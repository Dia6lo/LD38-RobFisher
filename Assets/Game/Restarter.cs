using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Restarter : MonoBehaviour {
    public static Restarter instance;
    public float TimeToRestart = 3f;
    public float timer = 0f;
    public bool hasVictory = false;

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else 
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            target = cameraWinTarget.transform.position;
            target.z = Camera.main.transform.position.z;
        }
    }
    
    public Transform cameraWinTarget;
    private Vector3 target;
    public float cameraWinTime = 2f;
    private Vector3 velocity = Vector3.zero;
    void Update()
    {
        if (hasVictory==false)
            timer += Time.deltaTime;  
        else
            Camera.main.transform.position = Vector3.SmoothDamp(
            Camera.main.transform.position,
            target, ref velocity, cameraWinTime);      
    }
    
    public void Restart()
    {
        StartCoroutine(RestartAfterPause(TimeToRestart));
    }
    
    public void Win()
    {
        hasVictory = true;
    }
  
    IEnumerator RestartAfterPause(float pause)
    {
        yield return new WaitForSecondsRealtime(pause);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
