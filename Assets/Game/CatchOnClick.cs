using System.Collections;
using UnityEngine;

public class CatchOnClick : MonoBehaviour
{
    private bool alreadyCatching;
    public FishingRod FishingRod;
    public Transform DropPoint;
    public float FlyingSpeed;

    private void Update()
    {
        if (alreadyCatching) return;
        if (!Input.GetButton("Fire1")) return;
        if (FishingRod.CaughtObject == null) return;
        CatchObject(FishingRod.CaughtObject);
    }

    private void CatchObject(GameObject go)
    {
        go.GetComponent<MoveInDirection>().enabled = false;
        StartCoroutine(FlyUp(go));
    }

    private IEnumerator FlyUp(GameObject go)
    {
        var reachedTarget = false;
        while (!reachedTarget)
        {
            var target = DropPoint.WorldPosition();
            var current = go.WorldPosition();
            var range = target - current;
            go.transform.Translate(range.normalized * FlyingSpeed * Time.deltaTime);
            reachedTarget = Vector3.Distance(DropPoint.WorldPosition(), go.WorldPosition()) < 1;
            yield return new WaitForEndOfFrame();
        }
    }
}

public static class GameObjectExtensions
{
    public static Vector3 WorldPosition(this GameObject gameObject)
    {
        return gameObject.transform.WorldPosition();
    }

    public static Vector3 WorldPosition(this Transform transform)
    {
        return transform.TransformPoint(Vector3.zero);
    }
}