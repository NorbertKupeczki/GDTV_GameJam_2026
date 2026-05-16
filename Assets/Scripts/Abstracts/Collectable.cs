using UnityEngine;
using UnityEngine.Assertions;

public abstract class Collectable : MonoBehaviour, ICollectable
{
    [SerializeField] protected GameEnums.CollectableTypes m_CollectableType = GameEnums.CollectableTypes.Undefined;
    
    public GameEnums.CollectableTypes CollectableType => m_CollectableType;

    protected Rigidbody m_Rigidbody;
    
    protected virtual void Awake()
    {
        if (!TryGetComponent(out m_Rigidbody))
        {
            Debug.LogError($"Collectable {gameObject.name} is missing a rigidbody!");
        }
    }
    
    protected virtual void Start()
    {
        Assert.IsFalse(m_CollectableType is GameEnums.CollectableTypes.Undefined, $"Collectable type of {name} is undefined!");
    }
    
    public abstract void Collect(Transform parent);
    
    public abstract void Drop();

    public abstract void Use();
}
