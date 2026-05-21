using UnityEngine;

public class SwitchButton : Usable
{
    private void Awake()
    {
        OnUse += HandleOnUse;
    }

    private void OnDestroy()
    {
        OnUse -= HandleOnUse;
    }
    
    private void HandleOnUse()
    {
        
    }
    
}
