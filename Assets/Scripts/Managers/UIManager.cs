using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private TMP_Text m_InteractionText;

    private const string INTERACTION_PICKUP = "Pick up";
    private const string INTERACTION_DRAIN = "Drain";
    private const string INTERACTION_CHARGE = "Charge";
    private const string INTERACTION_INSERT = "Insert";
    
    protected override void Awake()
    {
        base.Awake();
        
        ToggleInteractionText(false, GameEnums.InteractionType.None);
    }

    private void Start()
    {
        PlayerManager.Instance.OnInteractableSelected += ToggleInteractionText;
    }

    private void OnDestroy()
    {
        PlayerManager.Instance.OnInteractableSelected -= ToggleInteractionText;
    }
    
    private void ToggleInteractionText(bool toggle, GameEnums.InteractionType interactionType)
    {
        if (m_InteractionText.gameObject.activeSelf == toggle) { return; }
        
        m_InteractionText.gameObject.SetActive(toggle);

        m_InteractionText.text = interactionType switch
        {
            GameEnums.InteractionType.None => "",
            GameEnums.InteractionType.Pickup => INTERACTION_PICKUP,
            GameEnums.InteractionType.Drain  => INTERACTION_DRAIN,
            GameEnums.InteractionType.Charge => INTERACTION_CHARGE,
            GameEnums.InteractionType.Insert => INTERACTION_INSERT,
            _ => throw new ArgumentOutOfRangeException(nameof(interactionType), interactionType, null)
        };
    }
}
