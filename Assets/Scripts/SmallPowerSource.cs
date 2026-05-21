using UnityEngine;

public class SmallPowerSource : Drainable
{
    public override void MarkObject()
    {
        Debug.Log("Marking small power source");
    }

    public override void UnmarkObject()
    {
        Debug.Log("Unmarking small power source");
    }
}
