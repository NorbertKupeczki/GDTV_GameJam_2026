using UnityEngine;

public class BatteryCharger : Drainable
{
    [Header("Battery Charger")]
    [SerializeField] private uint m_AmountOfCharges;
    
    public override uint DrainPower()
    {
        if (m_AmountOfCharges <= 0) { return 0; }
        
        m_AmountOfCharges--;
        if (m_AmountOfCharges == 0) { m_IsDrainable = false;}
        
        return m_DrainableAmount;
    }

    public override void MarkObject()
    {
        
    }

    public override void UnmarkObject()
    {
        
    }
}
