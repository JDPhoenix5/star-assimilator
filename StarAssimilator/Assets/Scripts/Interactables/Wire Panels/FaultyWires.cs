using Unity.VisualScripting;
using UnityEngine;

public class FaultyWires : WirePanel
{
    private bool onFire, extinguished;
    private ParticleSystem smoke;
    private void Start()
    {
        cooldownLength = 30;
    }

    public override GameObject Interact(Equippable equipped)
    {
        if (!onFire)
        {
            if (equipped.GetComponent<ScrewDriver>() && interactable)
            {
                StartCoroutine(interactCooldown(cooldownLength));
                opened = !opened;
                if (opened)
                {
                    if (!extinguished)
                    {
                        onFire = true;
                        smoke.gameObject.SetActive(true);
                    }
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
        }
        else
        {
            if (equipped.GetComponent<FireExtinguisher>())
            {

            }
        }

            return Interact();
    }
    
    public override void OnOpened()
    {
        if (onFire)
        {
            Debug.Log("AHH FIREE");
        }
        else
        {
            Debug.Log("a little charred but otherwise ok");
        }
    }
    public override void OnClosed()
    {

    }

}
