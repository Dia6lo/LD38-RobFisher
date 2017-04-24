using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChangeTypeOnContact : MonoBehaviour
{
    public HouseWell HouseWell;
    private bool alreadyTouchedWater;
    private List<GameObject> currentCollisions = new List<GameObject>();
    private float sinceTouch;
    private const float AttachDelay = 3f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!enabled) return;
        HouseWell.Remove(gameObject);
        currentCollisions.Add(other.gameObject);
        SetStatus("Touching");
    }

    private void SetStatus(string status)
    {
        gameObject.SetLayerRecoursively(status);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!enabled) return;
        if (alreadyTouchedWater) return;
        if (!other.gameObject.CompareTag("Water")) return;
        // TODO: Splash
        HouseWell.Remove(gameObject);
        alreadyTouchedWater = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (!enabled) return;
        currentCollisions.Remove(other.gameObject);
        SetStatus(currentCollisions.Any() ? "Touching" : "Flying");
    }

    private void Update()
    {
        if (!currentCollisions.Any())
        {
            sinceTouch = 0;
            return;
        }
        sinceTouch += Time.deltaTime;
        if (sinceTouch > AttachDelay)
        {
            var rb = GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            enabled = false;
        }
    }
}