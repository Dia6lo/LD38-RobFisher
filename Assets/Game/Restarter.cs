using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Restarter : MonoBehaviour {
    public static Restarter instance;
    public float TimeToRestart = 3f;
    public float timer = 0f;
    public bool hasVictory = false;
    public TutorialPlank TutorialPlank;

    void Awake()
    {
        if (instance != null && instance != this){
            SavableObjects.transform.position = instance.savedPosition;
            Destroy(gameObject);
            Destroy(TutorialPlank);
        }
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

    Vector3 savedPosition = Vector3.zero;
    public Transform SavableObjects;
    public void Save(Vector3 position)
    {
        savedPosition = new Vector3(0, position.y + 1.5f, 0);
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
