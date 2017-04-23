using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FishingRod : MonoBehaviour
{
    private List<GameObject> caughtObjects = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Floating"))
            caughtObjects.Add(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Floating"))
            caughtObjects.Remove(other.gameObject);
    }

    public void Remove(GameObject go)
    {
        caughtObjects.Remove(go);
    }

    public GameObject CaughtObject
    {
        get { return caughtObjects.FirstOrDefault(); }
    }
}