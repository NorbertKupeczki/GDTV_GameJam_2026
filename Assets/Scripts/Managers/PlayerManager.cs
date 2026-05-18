using System;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    public event Action<bool> OnInteractableSelected;
    
    private PlayerInteractions m_Interactions;
    
    protected override void Awake()
    {
        base.Awake();

        if (!TryGetComponent<PlayerInteractions>(out m_Interactions))
        {
            Debug.LogError($"PlayerManager: No PlayerInteractions component found on {gameObject.name}");
            return;
        }
        
        m_Interactions.SetInteractionDelegate(SignalOnInteractableSelected);
    }

    private void SignalOnInteractableSelected(bool interactable)
    {
        OnInteractableSelected?.Invoke(interactable);
    }
}
