using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DieOnTouchingWater : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!enabled) return;
        if (other.CompareTag("Death"))
        {
            animator.SetTrigger("Sink");
            enabled = false;
            gameObject.DisableAllBehaviors();
            transform.parent.gameObject.DisableAllBehaviors();
        }
    }
}