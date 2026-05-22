using System;
using System.Collections;
using UnityEngine;

public class PlayerBattery : MonoBehaviour
{
    // The delegate passes the normalized battery level
    public event Action<float> OnBatteryChargeChanged;
    public event Action OnBatteryIsFlat;

    private const float MAX_BATTERY_CHARGE = 1.0f;
    private const float DRAIN_INTERVAL = 0.1f;
    private const float DRAIN_RATE = -0.001f;
    
    private float m_BatteryLevel;
    
    private Coroutine m_AutoDrainBatteryRoutine;

    private void Start()
    {
        m_BatteryLevel = MAX_BATTERY_CHARGE;
        
        m_AutoDrainBatteryRoutine = StartCoroutine(AutoDrainBattery());
    }
    
    public void ChangeBatteryCharge(float amount)
    {
        m_BatteryLevel = Mathf.Clamp(m_BatteryLevel + amount, 0.0f, MAX_BATTERY_CHARGE);
        OnBatteryChargeChanged?.Invoke(m_BatteryLevel);

        if (m_BatteryLevel > 0.0f) { return; }
        TriggerBatteryIsOutOfCharge();
    }

    public void StopAutoDrainBattery()
    {
        if (m_AutoDrainBatteryRoutine == null) { return; }
        
        StopCoroutine(m_AutoDrainBatteryRoutine);
        m_AutoDrainBatteryRoutine = null;
    }

    private IEnumerator AutoDrainBattery()
    {
        var delay = new WaitForSeconds(DRAIN_INTERVAL);
        
        while (m_BatteryLevel > 0.0f)
        {
            ChangeBatteryCharge(DRAIN_RATE);
            yield return delay;
        }
        
        OnBatteryChargeChanged?.Invoke(0.0f);
        yield return new WaitForEndOfFrame();
        TriggerBatteryIsOutOfCharge();
        
        yield return null;
    }
    
    private void TriggerBatteryIsOutOfCharge()
    {
        StopAutoDrainBattery();
        OnBatteryIsFlat?.Invoke();
    }
}
