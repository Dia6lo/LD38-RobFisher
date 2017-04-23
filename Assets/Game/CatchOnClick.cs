using System.Collections;
using UnityEngine;

public class CatchOnClick : MonoBehaviour
{
    private bool alreadyCatching;
    public Animator Animator;
    public FishingRod FishingRod;
    public Transform DropPoint;
    public float FlyingSpeed;

    private void Update()
    {
        if (alreadyCatching) return;
        if (!Input.GetButtonDown("Fire1")) return;
        var caughtObject = FishingRod.CaughtObject;
        if (caughtObject == null) return;
        FishingRod.Remove(caughtObject);
        CatchObject(caughtObject);
    }

    private void CatchObject(GameObject go)
    {
        Animator.SetTrigger("Catch");
        StartCoroutine(FlyUp(go));
    }

    private IEnumerator FlyUp(GameObject go)
    {
        alreadyCatching = true;
        go.GetComponent<MoveInDirection>().enabled = false;
        go.SetCollidersEnabled(false);
        yield return new WaitForSeconds(0.5f);
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
        go.SetCollidersEnabled(true);
        go.transform.SetParent(transform.parent.parent, true);
        go.transform.localPosition = go.transform.localPosition - Vector3.forward * go.transform.localPosition.z;
        go.layer = LayerMask.NameToLayer("Volatile Objects");
        var rb = go.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0.5f;
        rb.bodyType = RigidbodyType2D.Dynamic;
        go.AddComponent<FixOnWaterContact>();
        alreadyCatching = false;
    }
}

public static class GameObjectExtensions
{
    public static void SetCollidersEnabled(this GameObject gameObject, bool value)
    {
        foreach (var component in gameObject.GetComponents<Collider2D>())
        {
            component.enabled = value;
        }
    }

    public static void DisableAllBehaviors(this GameObject gameObject)
    {
        foreach (var component in gameObject.GetComponents<MonoBehaviour>())
        {
            component.enabled = false;
        }
    }

    public static Vector3 WorldPosition(this GameObject gameObject)
    {
        return gameObject.transform.WorldPosition();
    }

    public static Vector3 WorldPosition(this Transform transform)
    {
        return transform.TransformPoint(Vector3.zero);
    }
}