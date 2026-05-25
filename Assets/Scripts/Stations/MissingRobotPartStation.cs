using UnityEngine;

public class MissingRobotPartStation : Station
{
    [SerializeField] private Material m_RobotPartMaterial;
    [SerializeField] private SkinnedMeshRenderer m_RobotPartRenderer;
    
    public override bool DepositItem(Collectable item)
    {
        if (item.ItemData.CollectableType != m_DepositableItems.CollectableType) { return false; }
        
        Destroy(item.gameObject);
        
        m_RobotPartRenderer.material = m_RobotPartMaterial;
        
        HandleItemDeposited();
        return true;
    }

    public override void MarkObject()
    {
        UIManager.Instance.ToggleAuxiliaryText(true,$"Needs a {m_DepositableItems.ItemName}");
    }

    public override void UnmarkObject()
    {
        UIManager.Instance.ToggleAuxiliaryText(false);
    }
}
