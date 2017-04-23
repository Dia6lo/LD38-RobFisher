using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FixOnWaterContact : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    private List<GameObject> huggingHouseParts = new List<GameObject>();

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {if (other.gameObject.CompareTag("HousePart"))
        {
            huggingHouseParts.Add(other.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            rigidbody2D.isKinematic = true;
            rigidbody2D.gameObject.layer = LayerMask.NameToLayer("Watertouched Objects");
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("HousePart"))
        {
            huggingHouseParts.Remove(other.gameObject);
        }
    }

    private bool IsHuggingHouseParts
    {
        get { return huggingHouseParts.Any(); }
    }
}