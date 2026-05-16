using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoSingleton<InputManager>
{
    private GameInput m_GameInput;

    public event Action OnActionPressed;

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
        OnActionPressed += () => Debug.Log("OnActionPressed");
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
    }

    private void HandleActionPerformed(InputAction.CallbackContext obj)
    {
        OnActionPressed?.Invoke();
    }
    
    public Vector3 GetMovementVectorNormalized()
    {
        var inputVector = m_GameInput.Game.Move.ReadValue<Vector2>();
        return new Vector3(inputVector.x, 0, inputVector.y);
    }

    // public float GetRotationNormalized()
    // {
    //     return m_GameInput.Game.Rotation.ReadValue<float>();
    // }
    //
    // public float GetElevationNormalized()
    // {
    //     return m_GameInput.Game.Elevation.ReadValue<float>();
    // }

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
