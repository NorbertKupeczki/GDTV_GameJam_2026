using UnityEngine;
using UnityEngine.Assertions;

public abstract class Collectable : MonoBehaviour, IInteractable
{
    [SerializeField] protected ItemDataSO m_ItemData = null;
    
    public ItemDataSO ItemData => m_ItemData;

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
        Assert.IsNotNull(m_ItemData, $"ItemData on {name} is null!");
        Assert.IsFalse(ItemData.CollectableType is GameEnums.CollectableTypes.Undefined, $"Collectable type of {name} is undefined!");
    }
    
    public abstract void Collect(Transform parent);
    
    public abstract void Drop();

    public abstract void Use();
    
    public void MarkObject()
    {
        Debug.Log($"Mark Object {name}");
    }

    public void UnmarkObject()
    {
        Debug.Log($"Unmark Object {name}");
    }
}
