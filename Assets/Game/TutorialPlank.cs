using System;
using System.Collections;
using UnityEngine;

public class TutorialPlank : MonoBehaviour
{
    public AudioSource Music;
    public Sink Sinking;
    public SpawnNewHouseParts Spawner;
    public HouseWell HouseWell;
    public Transform Title;

    private void Start()
    {
        Sinking.enabled = false;
        Spawner.enabled = false;
        HouseWell.enabled = false;
        SetTitleAlpha(0);
    }

    public void Activate()
    {
        Music.Play();
        Sinking.enabled = true;
        Spawner.enabled = true;
        HouseWell.enabled = true;
        StartCoroutine(FadeInTitle());
    }

    private IEnumerator FadeInTitle()
    {
        var alpha = 0f;
        while (alpha < 255)
        {
            SetTitleAlpha(alpha);
            yield return new WaitForEndOfFrame();
            alpha += Time.deltaTime * 0.75f;
        }
        SetTitleAlpha(255);
    }

    private void SetTitleAlpha(float value)
    {
        var texts = Title.GetComponentsInChildren<TextMesh>();
        foreach (var text in texts)
        {
            var color = text.color;
            color.a = value;
            text.color = color;
        }
    }
}