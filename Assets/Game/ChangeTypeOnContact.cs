using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChangeTypeOnContact : MonoBehaviour
{
    private bool alreadyTouchedWater;
    private List<GameObject> currentCollisions = new List<GameObject>();

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!enabled) return;
        currentCollisions.Add(other.gameObject);
        SetStatus("Touching");
    }

    private void SetStatus(string status)
    {
        gameObject.layer = LayerMask.NameToLayer(status);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!enabled) return;
        if (alreadyTouchedWater) return;
        if (!other.gameObject.CompareTag("Water")) return;
        // TODO: Splash
        alreadyTouchedWater = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (!enabled) return;
        currentCollisions.Remove(other.gameObject);
        SetStatus(currentCollisions.Any() ? "Touching" : "Flying");
    }
}