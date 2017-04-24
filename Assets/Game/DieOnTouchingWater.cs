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
        
        enabled = false;
        gameObject.DisableAllBehaviors();
        transform.parent.gameObject.DisableAllBehaviors();
        var rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        
        if (other.CompareTag("Water"))
        {
            animator.SetTrigger("Sink");  
            if (Restarter.instance)
                Restarter.instance.Restart();
        }
        
        if (other.CompareTag("Heaven"))
        {
            Camera.main.transform.root.GetComponent<Sink>().enabled = false;
            animator.SetTrigger("Happy");
            if (Restarter.instance)
                Restarter.instance.Win();            
        }        
    }
}