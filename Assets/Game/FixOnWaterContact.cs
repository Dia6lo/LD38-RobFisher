using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FixOnWaterContact : MonoBehaviour
{
    private bool alreadyTouchedWater;
    private new Rigidbody2D rigidbody2D;
    private List<GameObject> currentCollisions = new List<GameObject>();

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        currentCollisions.Add(other.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (alreadyTouchedWater) return;
        if (!other.gameObject.CompareTag("Water")) return;
        alreadyTouchedWater = true;
        rigidbody2D.isKinematic = true;
        if (IsHuggingHouseParts)
        {
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            rigidbody2D.gameObject.layer = LayerMask.NameToLayer("Watertouched Objects");
        }
        else
        {
            rigidbody2D.velocity = Vector2.down;
            SinkWithoutHouse();
        }
    }

    private void SinkWithoutHouse()
    {
        // TODO: Splash
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        currentCollisions.Remove(other.gameObject);
    }

    private bool IsHuggingHouseParts
    {
        get {
            return currentCollisions.Any(c =>
                c.layer == LayerMask.NameToLayer("House Objects") ||
                c.layer == LayerMask.NameToLayer("Watertouched Objects"));
        }
    }
}