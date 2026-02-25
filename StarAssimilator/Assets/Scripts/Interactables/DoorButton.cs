using System.Collections;
using UnityEngine;

public class DoorButton : Interaction
{
    public Animator animator;
    public Collider wall;
    private bool opening = false;
    public override GameObject Interact()
    {
        animator.SetTrigger("Open");
        if (!opening)
        {
            StartCoroutine(CloseTimer());
        }
        return null;
    }
    public override GameObject Interact(Equippable go)
    {
        return Interact();
    }
    private IEnumerator CloseTimer()
    {
        opening = true;
        yield return new WaitForSeconds(1.15f);
        wall.enabled = false;
        yield return new WaitForSeconds(5);
        animator.SetTrigger("Close");
        wall.enabled = true;
        opening = false;
    }
}
