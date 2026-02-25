using System.Collections;
using UnityEngine;

public abstract class WirePanel : Interaction
{
    public MeshRenderer defaultMesh, openedMesh;
    protected bool opened, interactable = true;
    public float cooldownLength = 4;
    public override GameObject Interact()
    {
        Debug.Log("I need a screwdiver...");//put into dialogue system
        return null;
    }
    public override GameObject Interact(Equippable equipped)
    {
        if (equipped.GetComponent<ScrewDriver>() && interactable)
        {
            StartCoroutine(interactCooldown(cooldownLength));
            opened = !opened;
            if (opened)
            {
                OnOpened();
                defaultMesh.enabled = false;
                openedMesh.enabled = true;
            }
            else
            {
                OnClosed();
                defaultMesh.enabled = true;
                openedMesh.enabled = false;
            }
            return null;
        }
        return Interact();
    }
    public abstract void OnOpened();
    public abstract void OnClosed();
    public IEnumerator interactCooldown(float time)
    {
        interactable = false;
        yield return new WaitForSeconds(time);
        interactable = true;
    }
}
