using System;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private CinemachineCamera m_Camera;
    [Header("Sphere cast")]
    [SerializeField] private float m_SphereRadius = 0.25f;
    [SerializeField] private float m_MaxInteractionDistance = 1.0f;
    [SerializeField] private LayerMask m_InteractableLayer;

    [Header("Item carried")]
    [SerializeField] private Transform m_HeldItemTransform;
    private IInteractable m_TargetInteractable;
    [SerializeField] private Collectable m_HeldItem;

    private Action<bool, GameEnums.InteractionType> m_SignalInteraction;
    
    private void Start()
    {
        InputManager.Instance.OnPickDropPressed += HandlePickDrop;
        InputManager.Instance.OnActionPressed += HandleAction;
    }

    private void OnDestroy()
    {
        InputManager.Instance.OnPickDropPressed -= HandlePickDrop;
        InputManager.Instance.OnActionPressed -= HandleAction;
    }
    
    private void Update()
    {
        CheckForInteractable();
    }

    private void CheckForInteractable()
    {
        Debug.DrawLine(
            m_Camera.transform.position,
            m_Camera.transform.position + m_Camera.transform.forward * m_MaxInteractionDistance,
            Color.red);

        Physics.SphereCast(
            m_Camera.transform.position,
            m_SphereRadius,
            m_Camera.transform.forward,
            out var raycastHit,
            m_MaxInteractionDistance,
            m_InteractableLayer);

        if (!raycastHit.collider || !raycastHit.collider.TryGetComponent(out IInteractable interactable))
        {
            if (m_TargetInteractable == null) return;
            
            m_TargetInteractable.UnmarkObject();
            m_SignalInteraction?.Invoke(false, GameEnums.InteractionType.None);
            m_TargetInteractable = null;
            return;
        }

        if (m_TargetInteractable != null)
        {
            if (m_TargetInteractable.InteractableGameObject == interactable.InteractableGameObject) return;
            
            m_TargetInteractable.UnmarkObject();
            MarkNewInteractable(interactable);
            return;
        }
        
        MarkNewInteractable(interactable);
        
        //Debug.Log(collectable);
        return;
        
        // LOCAL FUNCTIONS \\
        void MarkNewInteractable(IInteractable newInteractable)
        {
            m_TargetInteractable = newInteractable;
            m_TargetInteractable.MarkObject();
            // TODO: If interaction type is Insert, check whether the object held can be inserted first
            if (m_TargetInteractable.InteractionType == GameEnums.InteractionType.Drain)
            {
                var drainable = m_TargetInteractable as Drainable;
                if (drainable && !drainable.IsDrainable) { return; }
            }
            m_SignalInteraction?.Invoke(true, newInteractable.InteractionType);
        }
    }
    
    private void HandleAction()
    {
        switch (m_TargetInteractable)
        {
            case null:
                return;
            case Usable usable:
                usable.Use();
                break;
            case Drainable drainable:
            {
                if (!drainable.IsDrainable) { break; }
                PlayerManager.Instance.ChargePlayerBattery(drainable.DrainPower());
                if (!drainable.IsDrainable)
                {
                    m_SignalInteraction?.Invoke(false, GameEnums.InteractionType.None);
                }
                break;
            }
            case Chargeable chargeable:
                chargeable.ChargeObject();
                break;
            case Station station when m_HeldItem:
                var isSuccessful = station.DepositItem(m_HeldItem); // Do something with the boolean
                break;
        }
    }

    private void HandlePickDrop()
    {
        if (m_HeldItem)
        {
            m_HeldItem.Drop(m_Camera.transform.position + m_Camera.transform.forward);
            m_HeldItem = null;
            return;
        }

        if (m_TargetInteractable == null) { return; }
        
        var collectable = m_TargetInteractable as Collectable;
        if (!collectable) { return; }
        
        m_HeldItem = collectable;
        m_HeldItem?.Collect(m_HeldItemTransform);
        //m_TargetInteractable = null;
    }

    public void SetInteractionDelegate(Action<bool, GameEnums.InteractionType> delegateFunction)
    {
        m_SignalInteraction = delegateFunction;
    }
}
