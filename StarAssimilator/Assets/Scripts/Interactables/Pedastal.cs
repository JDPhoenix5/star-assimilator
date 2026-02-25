using UnityEngine;

public class Pedastal : Interaction
{
    public GameObject holyObject;

    public override GameObject Interact()
    {
        return holyObject;
    }
    public override GameObject Interact(Equippable equipment)
    {
        if (equipment == null)
        {
            return Interact();
        }
        GameObject thing = holyObject;
        holyObject = equipment.gameObject;
        return thing;
    }
}
