using UnityEngine;

public class PlayTimerView : MonoBehaviour {
    Restarter restarter;
    TextMesh text;
    
    void Start()
    {
        if (Restarter.instance == null)
            Destroy(this);
        else
        {
            restarter = Restarter.instance;
            text = GetComponent<TextMesh>();
        }
    }

    void Update()
    {
        if (restarter.hasVictory)
        {
            text.text = "Your time: "+  ((int)restarter.timer)  +" sec.";
            this.enabled = false;
        }
    }
}
