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
    private Collectable m_TargetCollectable;
    [SerializeField] private Collectable m_HeldItem;

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

        if (!raycastHit.collider || !raycastHit.collider.TryGetComponent(out Collectable collectable))
        {
            //Debug.Log("No interactable found");
            m_TargetCollectable = null;
            return;
        }
        
        m_TargetCollectable = collectable;
        //Debug.Log(collectable);
    }
    
    private void HandleAction()
    {
        Debug.Log("Handle Action || Yet Unimplemented...");
    }

    private void HandlePickDrop()
    {
        if (m_HeldItem)
        {
            m_HeldItem.Drop();
            m_HeldItem = null;
            return;
        }
        
        m_HeldItem = m_TargetCollectable;
        m_TargetCollectable?.Collect(m_HeldItemTransform);
        m_TargetCollectable = null;
    }
}
