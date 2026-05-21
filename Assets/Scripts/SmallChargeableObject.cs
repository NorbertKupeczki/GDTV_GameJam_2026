using UnityEngine;

public class SmallChargeableObject : Chargeable
{
    public override void MarkObject()
    {
        Debug.Log("Marking small chargeable object");
    }

    public override void UnmarkObject()
    {
        Debug.Log("Marking small chargeable object");
    }
}
