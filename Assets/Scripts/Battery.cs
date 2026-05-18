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

    public override void Drop()
    {
        base.Drop();
    }

    public override void Use()
    {
        Debug.Log("Battery used");
    }
}
