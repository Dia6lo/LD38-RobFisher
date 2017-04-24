using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DieOnTouchingWater : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void DisableStuff(){
        enabled = false;
        gameObject.DisableAllBehaviors();
        transform.parent.gameObject.DisableAllBehaviors();
        var rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;    
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!enabled) return;
              
        if (other.CompareTag("Water"))
        {
            animator.SetTrigger("Sink"); 
            DisableStuff();
            if (Restarter.instance)
                Restarter.instance.Restart();
        }
        
        if (other.CompareTag("Heaven"))
        {
            Camera.main.transform.root.GetComponent<Sink>().enabled = false;
            animator.SetTrigger("Happy");
            DisableStuff();
            if (Restarter.instance)
                Restarter.instance.Win();            
        } 


          
    }
}