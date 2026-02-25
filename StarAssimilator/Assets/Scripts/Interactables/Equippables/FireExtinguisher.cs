using UnityEngine;

public class FireExtinguisher : Equippable
{
    public ParticleSystem extinguishSystem;
    public new void Use()
    {
        animator.enabled = true;
        AnimateHold();
        extinguishSystem.gameObject.SetActive(true);
    }
    public new void Unuse()
    {
        UnAnimate();
        animator.enabled = false;
        extinguishSystem.gameObject.SetActive(false);
    }
}
