using UnityEngine;

public class ScrewDriver : Equippable
{
    public new void Use()
    {
        AnimateOnce();
        Debug.Log("Used");
    }
}
