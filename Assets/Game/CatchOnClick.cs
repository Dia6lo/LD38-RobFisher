using System.Collections;
using UnityEngine;

public class CatchOnClick : MonoBehaviour
{
    private bool alreadyCatching;
    public PhysicsMaterial2D PhysMaterialForBricks;
    public Animator Animator;
    public FishingRod FishingRod;
    public Transform DropPoint;
    public HouseWell HouseWell;
    public float FlyingSpeed;
    public ParticleSystem SplashEffect;
    public AudioSource SplashSound;

    private void Update()
    {
        if (alreadyCatching) return;
        if (!Input.GetButtonDown("Fire1")) return;
        var caughtObject = FishingRod.CaughtObject;
        if (caughtObject == null) return;
        CatchObject(caughtObject);
    }

    private void CatchObject(GameObject go)
    {
        Animator.SetTrigger("Catch");
        StartCoroutine(FlyUp(go));
    }

    private void PlaySplashEffect(Vector3 position)
    {
        SplashEffect.transform.position = position;
        SplashEffect.Play();
        SplashSound.Play();
    }


    private IEnumerator FlyUp(GameObject go)
    {
        alreadyCatching = true;
        go.SetLayerRecoursively("Flying");
        FishingRod.Remove(go);
        go.DisableAllBehaviors();
        go.SetCollidersEnabled(false);
        yield return new WaitForSeconds(0.5f);
        PlaySplashEffect(go.transform.position);
        var reachedTarget = false;
        while (!reachedTarget)
        {
            var target = DropPoint.WorldPosition();
            var current = go.WorldPosition();
            var range = target - current;
            go.transform.Translate(range.normalized * FlyingSpeed * Time.deltaTime);
            reachedTarget = Vector3.Distance(DropPoint.WorldPosition(), go.WorldPosition()) < 1;
            yield return new WaitForEndOfFrame();
        }
        go.SetCollidersEnabled(true);
        go.transform.SetParent(transform.parent, true);
        go.transform.localPosition = go.transform.localPosition - Vector3.forward * go.transform.localPosition.z;
        var rb = go.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0.1f;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.sharedMaterial = PhysMaterialForBricks;
        var cf = go.AddComponent<ConstantForce2D>();
        if (go.name != "TutorialPlank")
            cf.force = new Vector2(-0.2f,0);
            
        var changeTypeOnContact = go.AddComponent<ChangeTypeOnContact>();
        changeTypeOnContact.HouseWell = HouseWell;
        changeTypeOnContact.Splash = SplashEffect;
        changeTypeOnContact.SplashSound = SplashSound;
        alreadyCatching = false;
        HouseWell.Add(go);
    }
}