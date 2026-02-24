using UnityEngine;

public abstract class Equippable : MonoBehaviour
{
    [SerializeField] private GameObject equipObject;
    [SerializeField] private TagHandle usableOn;
    [SerializeField] private Animator animator;
    public void Use()
    {

        Debug.Log("Used");
    }
    public void Unuse()
    {
        UnAnimate();
        Debug.Log("Unused");
    }
    public void AnimateOnce()
    {
        animator.SetBool("use", true);
        animator.SetTrigger("use once");
    }
    public void AnimateHold()
    {
        animator.SetBool("use", true);
        animator.SetBool("use continuous", true);
    }
    public void UnAnimate()
    {
        animator.SetBool("use", false);
        animator.SetBool("use continuous", false);

    }
}
