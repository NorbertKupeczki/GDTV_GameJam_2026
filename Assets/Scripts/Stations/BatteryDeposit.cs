using UnityEngine;

public class BatteryDeposit : Station
{
    protected override void Start()
    {
        
    }

    public override bool DepositItem(Collectable item)
    {
        if (item.ItemData.CollectableType != m_DepositableItems.CollectableType) { return false; }

        m_DepositedItem = item;
        m_DepositedItem.transform.SetParent(m_DepositableItemParent);
        
        m_DepositedItem.transform.position = m_DepositableItemParent.position;
        m_DepositedItem.transform.rotation = m_DepositableItemParent.rotation;
        
        HandleItemDeposited();
        return true;
    }

    public override void MarkObject()
    {
        if (m_DepositedItem) { return; }
        
        UIManager.Instance.ToggleAuxiliaryText(true,$"Needs a {m_DepositableItems.ItemName}");
    }

    public override void UnmarkObject()
    {
        UIManager.Instance.ToggleAuxiliaryText(false);
    }
}
