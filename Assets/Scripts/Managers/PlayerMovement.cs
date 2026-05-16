using UnityEngine;

public class PlayerMovement : MonoSingleton<PlayerMovement>
{
    [SerializeField] private float m_MoveSpeed = 500f;
    
    private Rigidbody m_Rigidbody;

    private void Awake()
    {
        if (!TryGetComponent<Rigidbody>(out m_Rigidbody))
        {
            Debug.LogError($"<color=red><b>PlayerMovement</color></b> >> No rigidbody found on {name}");
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        HandleMovement();
        
    }

    private void HandleMovement()
    {
        m_Rigidbody.linearVelocity = InputManager.Instance.GetMovementVectorNormalized() * (m_MoveSpeed * Time.deltaTime);
    }
}
