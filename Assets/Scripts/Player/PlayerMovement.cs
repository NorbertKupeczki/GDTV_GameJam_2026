using Unity.Cinemachine;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float m_MoveSpeed = 500f;
    [SerializeField] private float m_RotateSpeed = 10f;
    [SerializeField] private float m_JumpForce = 5f;
    [SerializeField] private CinemachineCamera m_Camera;
    
    private Rigidbody m_Rigidbody;
    private float m_CurrentXRotation = 0f;
    private bool m_IsGrounded = false;

    private const float MAX_CAMERA_ROTATION = 60;

    private void Awake()
    {
        if (!TryGetComponent<Rigidbody>(out m_Rigidbody))
        {
            Debug.LogError($"<color=red><b>PlayerMovement</color></b> >> No rigidbody found on {name}");
        }
        
        m_Rigidbody.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        InputManager.Instance.OnJumpPressed += HandleJump;
    }

    private void OnDestroy()
    {
        InputManager.Instance.OnJumpPressed -= HandleJump;
    }

    // Update is called once per frame
    private void Update()
    {
        HandleLook();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }
    
    private void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (!(Vector3.Angle(contact.normal, Vector3.up) < 45f)) continue;
            
            m_IsGrounded = true;
            return;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        m_IsGrounded = false;
    }

    private void HandleMovement()
    {
        Vector3 moveInput = InputManager.Instance.GetMovementVectorNormalized();
        Vector3 targetVelocity = (transform.forward * moveInput.z + transform.right * moveInput.x) * m_MoveSpeed;
        
        // Preserve the current Y velocity (gravity / jumping)
        targetVelocity.y = m_Rigidbody.linearVelocity.y;

        if (!m_IsGrounded) { return; }
        m_Rigidbody.linearVelocity = targetVelocity;
    }

    private void HandleJump()
    {
        if (!m_IsGrounded) return;
        
        m_Rigidbody.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
        m_IsGrounded = false;
    }

    private void HandleLook()
    {
        Vector2 mouseInput = InputManager.Instance.GetLookNormalized();
        
        // Rotate player rigidbody
        float rotationAmount = mouseInput.x * m_RotateSpeed * Time.deltaTime;
        Quaternion deltaRotation = Quaternion.Euler(0f, rotationAmount, 0f);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * deltaRotation);
        
        // Tilt the camera
        m_CurrentXRotation -= mouseInput.y * m_RotateSpeed * Time.deltaTime;
        m_CurrentXRotation = Mathf.Clamp(m_CurrentXRotation, -MAX_CAMERA_ROTATION, MAX_CAMERA_ROTATION);
        m_Camera.transform.localRotation = Quaternion.Euler(m_CurrentXRotation, 0f, 0f);
    }
}
