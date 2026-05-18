using UnityEngine;

public class BatteryCharger : Station
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    public override bool DepositItem(Collectable item)
    {
        if (m_DepositableItems != item.ItemData) { return false; }
        
        //Do something with the item...
        m_DepositedItem = item;
        item.transform.parent = m_DepositableItemParent;
        item.transform.position = m_DepositableItemParent.position;
        return true;
    }

    public override Collectable RemoveItem()
    {
        Debug.Log("Removing Battery from Charger");
        return null; // HERE WE NEED TO RETURN THE OBJECT TO THE PLAYER, NULL FOR NOW...
    }

    // Update is called once per frame
    void Update()
    {
        //Do something if it has a not fully charged battery
    }
}
