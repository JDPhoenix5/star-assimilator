using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Interaction : MonoBehaviour
{
    public abstract GameObject Interact();
    public abstract GameObject Interact(Equippable equipped);
}
