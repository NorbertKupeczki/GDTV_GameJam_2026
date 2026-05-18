using UnityEngine;

public class Battery : Collectable
{
    protected override void Start()
    {
        base.Start();
    }
    
    public override void Collect(Transform parent)
    {
        base.Collect(parent);
    }

    public override void Drop(Vector3 dropPosition)
    {
        base.Drop(dropPosition);
    }

    public override void Use()
    {
        Debug.Log("Battery used");
    }

    public override void MarkObject()
    {
        Debug.Log("Battery marked");
    }

    public override void UnmarkObject()
    {
        Debug.Log("Battery unmarked");
    }
}
