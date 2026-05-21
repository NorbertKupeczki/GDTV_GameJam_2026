using System;
using UnityEngine;

public abstract class Usable : MonoBehaviour, IInteractable
{
    public event Action OnUse;
    
    public void MarkObject()
    {
        
    }

    public void UnmarkObject()
    {
        
    }

    public void Use()
    {
        OnUse?.Invoke();
    }

    public GameEnums.InteractionType InteractionType => GameEnums.InteractionType.Use;
    public GameObject InteractableGameObject => gameObject;
}
