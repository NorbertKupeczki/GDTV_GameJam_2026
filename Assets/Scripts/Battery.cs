using UnityEngine;

public class Battery : Collectable
{
    protected override void Start()
    {
        base.Start();
    }
    
    public override void Collect(Transform parent)
    {
        m_Rigidbody.useGravity = false;
        m_Rigidbody.isKinematic = true;
        transform.parent = parent;
        transform.position = parent.position;
        transform.rotation = parent.rotation;
    }

    public override void Drop()
    {
        m_Rigidbody.useGravity = true;
        m_Rigidbody.isKinematic = false;
        transform.parent = null;
    }

    public override void Use()
    {
        Debug.Log("Battery collected");
    }
}
