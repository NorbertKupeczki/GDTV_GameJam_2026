using DG.Tweening;
using UnityEngine;

public class SlidingDoubleDoor : MonoBehaviour
{
    [SerializeField] private Usable m_TriggerObject;
    [Header("Doors")]
    [SerializeField] private Transform m_LeftDoor;
    [SerializeField] private Transform m_RightDoor;

    private const float DOOR_OPEN_DURATION = 1.0f;
    private bool m_DoorIsOpen;
    private bool m_DoorIsMoving;
    
    // Left Door
    private Vector3 m_LeftDoorClosedPosition;
    private Vector3 m_LeftDoorOpenPosition;
    
    // Right Door
    private Vector3 m_RightDoorClosedPosition;
    private Vector3 m_RightDoorOpenPosition;
    
    // Sound origin
    private Vector3 m_SoundOrigin;
    
    private void Awake()
    {
        m_DoorIsOpen = false;
        m_DoorIsMoving = false;
        Vector3 openVector = new(1.75f, 0.0f, 0.0f);

        m_SoundOrigin = transform.position + Vector3.up * 1.5f;
            
        m_LeftDoorClosedPosition = m_LeftDoor.localPosition;
        m_LeftDoorOpenPosition = m_LeftDoor.localPosition - openVector;
        
        m_RightDoorClosedPosition = m_RightDoor.localPosition;
        m_RightDoorOpenPosition = m_RightDoor.localPosition + openVector;
    }

    private void Start()
    {
        if (!m_TriggerObject) { return; }
        m_TriggerObject.OnUse += HandleDoorToggle;
    }

    private void OnDestroy()
    {
        if (!m_TriggerObject) { return; }
        m_TriggerObject.OnUse -= HandleDoorToggle;
    }

    private void HandleDoorToggle()
    {
        if (m_DoorIsOpen)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }
    }
    
    public void OpenDoor()
    {
        if (m_DoorIsMoving || m_DoorIsOpen) { return; }
        m_DoorIsMoving = true;
        
        AudioManager.Instance.PlayOneShotAudio(
            AudioLibrary.Instance.DoorOpen,
            m_SoundOrigin);
        
        m_LeftDoor.DOLocalMoveX(m_LeftDoorOpenPosition.x,DOOR_OPEN_DURATION)
            .SetEase(Ease.InOutSine)
            .OnComplete(() => m_DoorIsOpen = true);
        m_RightDoor.DOLocalMoveX(m_RightDoorOpenPosition.x,DOOR_OPEN_DURATION)
            .SetEase(Ease.InOutSine)
            .OnComplete(() => m_DoorIsMoving = false);
    }

    private void CloseDoor()
    {
        if (m_DoorIsMoving || !m_DoorIsOpen) { return; }
        m_DoorIsMoving = true;
        
        AudioManager.Instance.PlayOneShotAudio(
            AudioLibrary.Instance.DoorClose,
            m_SoundOrigin);
        
        m_LeftDoor.DOLocalMoveX(m_LeftDoorClosedPosition.x,DOOR_OPEN_DURATION)
            .SetEase(Ease.InOutSine)
            .OnComplete(() => m_DoorIsOpen = false);
        m_RightDoor.DOLocalMoveX(m_RightDoorClosedPosition.x,DOOR_OPEN_DURATION)
            .SetEase(Ease.InOutSine)
            .OnComplete(() => m_DoorIsMoving = false);
    }
}
