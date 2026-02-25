using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public abstract class Equippable : Interaction
{
    public Sprite equipSprite;
    public bool inUse;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer renderer;

    public void SetAnimator(Animator thingy, SpriteRenderer render)
    {
        animator = thingy;
        renderer = render;
        renderer.sprite = equipSprite;
    }
    public void Use()
    {
        AnimateHold();
        Debug.Log("Used");
    }
    public void Unuse()
    {
        UnAnimate();
        Debug.Log("Unused");
    }

    public override GameObject Interact()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        return gameObject;
    }
    public override GameObject Interact(Equippable equipped)
    {
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        equipped.Drop(transform);
        return gameObject;
    }
    public void Drop(Transform dropSpot)
    {
        gameObject.GetComponent<Collider>().enabled = true;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        transform.position = dropSpot.position;
        transform.rotation = dropSpot.rotation;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }

    #region animation
    public void AnimateOnce()
    {
        animator.SetTrigger("use once");
    }
    public void AnimateHold()
    {
        animator.SetBool("use continuous", true);
    }
    public void UnAnimate()
    {
        animator.SetBool("use continuous", false);

    }
    #endregion
}
