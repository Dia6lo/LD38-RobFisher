using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class Jump : MonoBehaviour
{
    public float Power;
    private bool isJumping;
    private Animator animator;
    private new Rigidbody2D rigidbody;
    private new Collider2D collider;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isJumping)
        {
            return;
        }
        if (!Input.GetButtonDown("Jump")) return;
        animator.SetTrigger("Jump");
        rigidbody.AddForce(new Vector2(0, Power));
        StartCoroutine(TriggerPhysicsOnApex());
        isJumping = true;
    }

    private IEnumerator TriggerPhysicsOnApex()
    {
        collider.enabled = false;
        while (!IsFlyingUp)
        {
            yield return new WaitForEndOfFrame();
        }
        while (IsFlyingUp)
        {
            yield return new WaitForEndOfFrame();
        }
        var rigidbodies = FindObjectsOfType<Rigidbody2D>();
        foreach (var rb in rigidbodies.Where(r => r.gameObject.layer == LayerMask.NameToLayer("Watertouched Objects")))
        {
            rb.gameObject.layer = LayerMask.NameToLayer("House Objects");
        }
        collider.enabled = true;
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