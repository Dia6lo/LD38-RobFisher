using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jump : MonoBehaviour
{
    public float Power;
    private bool isJumping;
    private new Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isJumping)
        {
            return;
        }
        if (!Input.GetButtonDown("Jump")) return;
        rigidbody.AddForce(new Vector2(0, Power));
        StartCoroutine(TriggerPhysicsOnApex());
        isJumping = true;

    }

    private IEnumerator TriggerPhysicsOnApex()
    {
        while (!IsFlyingUp)
        {
            yield return new WaitForEndOfFrame();
        }
        while (IsFlyingUp)
        {
            yield return new WaitForEndOfFrame();
        }
        var rigidbodies = FindObjectsOfType<Rigidbody2D>();
        foreach (var rb in rigidbodies)
        {
            if (rb.transform.position.y < transform.position.y)
            {
                rb.isKinematic = true;
                rb.gameObject.layer = LayerMask.NameToLayer("Fixed Objects");
            }
        }
    }

    private bool IsFlyingUp
    {
        get { return rigidbody.velocity.y > 0; }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        isJumping = false;
    }
}