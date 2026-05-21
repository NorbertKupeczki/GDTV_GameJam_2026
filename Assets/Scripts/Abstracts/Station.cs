using UnityEngine;
using UnityEngine.Assertions;

public abstract class Station : MonoBehaviour, IInteractable
{
    [SerializeField] protected ItemDataSO m_DepositableItems;
    [SerializeField] protected Transform m_DepositableItemParent;
    
    protected Collectable m_DepositedItem = null;
    
    protected virtual void Awake()
    {
        Assert.IsNotNull(m_DepositableItems, $"{name} >> DepositableItems cannot be null.");
        Assert.IsNotNull(m_DepositableItemParent, $"{name} >> m_DepositableItemParent cannot be null.");
    }

    protected virtual void Start()
    {
        
    }

    public abstract bool DepositItem(Collectable item);

    public abstract Collectable RemoveItem();
    
    public void MarkObject()
    {
        Debug.Log($"Mark Object {name}");
    }

    public void UnmarkObject()
    {
        Debug.Log($"Unmark Object {name}");
    }

    public GameEnums.InteractionType InteractionType => GameEnums.InteractionType.Insert;
    
    public GameObject InteractableGameObject => gameObject;
}
