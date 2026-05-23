using System;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    public event Action<bool, GameEnums.InteractionType> OnInteractableSelected;

    [SerializeField] private Animator m_ArmAnimator;
    private static readonly int ToggleArm = Animator.StringToHash("ToggleArm");
    
    private PlayerInteractions m_Interactions;
    private PlayerBattery m_Battery;
    
    
    public PlayerBattery GetBattery => m_Battery;
    
    protected override void Awake()
    {
        base.Awake();

        if (!TryGetComponent<PlayerInteractions>(out m_Interactions))
        {
            Debug.LogError($"PlayerManager: No PlayerInteractions component found on {gameObject.name}");
            return;
        }

        if (!TryGetComponent<PlayerBattery>(out m_Battery))
        {
            Debug.LogError($"PlayerManager: No PlayerBattery component found on {gameObject.name}");
            return;
        }
        
        m_Interactions.SetInteractionDelegate(SignalOnInteractableSelected);
    }

    private void Start()
    {
        InputManager.Instance.OnArmCheckPressed += ToggleArmAnimation;
    }

    private void OnDestroy()
    {
        InputManager.Instance.OnArmCheckPressed -= ToggleArmAnimation;
    }

    private void SignalOnInteractableSelected(bool interactable, GameEnums.InteractionType interactionType)
    {
        OnInteractableSelected?.Invoke(interactable, interactionType);
    }

    private void ToggleArmAnimation()
    {
        m_ArmAnimator.SetTrigger(ToggleArm);
    }

    public void ChargePlayerBattery(uint percent)
    {
        m_Battery.ChangeBatteryCharge(percent/100.0f);
    }

    public void DrainPlayerBattery(uint percent)
    {
        m_Battery.ChangeBatteryCharge(-percent/100.0f);
    }
}
