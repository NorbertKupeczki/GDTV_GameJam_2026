using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoSingleton<InputManager>
{
    private GameInput m_GameInput;

    public event Action OnActionPressed;
    public event Action OnJumpPressed;

    [Header("DEBUG")]
    [SerializeField] private Vector2 m_MoveVector;
    [SerializeField] private Vector2 m_Look;
    
    protected override void Awake()
    {
        base.Awake();
        
        InitialiseInputManager();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        m_MoveVector = Vector2.zero;
    }

    // Update is called once per frame
    private void Update()
    {
        m_MoveVector = m_GameInput.Game.Move.ReadValue<Vector2>();
        m_Look = m_GameInput.Game.Look.ReadValue<Vector2>();
    }
    
    private void InitialiseInputManager()
    {
        m_GameInput = new GameInput();
        m_GameInput.Game.Enable();
        
        m_GameInput.Game.Action.performed += HandleActionPerformed;
        m_GameInput.Game.Jump.performed += HandleJumpPerformed;
    }

    private void HandleActionPerformed(InputAction.CallbackContext obj)
    {
        Debug.Log("Action Pressed");
        OnActionPressed?.Invoke();
    }

    private void HandleJumpPerformed(InputAction.CallbackContext obj)
    {
        Debug.Log("Jump Pressed");
        OnJumpPressed?.Invoke();
    }
    
    public Vector3 GetMovementVectorNormalized()
    {
        var inputVector = m_GameInput.Game.Move.ReadValue<Vector2>();
        return new Vector3(inputVector.x, 0, inputVector.y);
    }

    public Vector2 GetLookNormalized()
    {
        return m_GameInput.Game.Look.ReadValue<Vector2>();
    }

    public void TogglePlayerControls(bool toggle)
    {
        if (toggle)
        {
            m_GameInput.Game.Enable();
        }
        else
        {
            m_GameInput.Game.Disable();
        }
    }
}
