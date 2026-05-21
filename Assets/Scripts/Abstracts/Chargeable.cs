using UnityEngine;

public abstract class Chargeable : MonoBehaviour, IInteractable
{
    [SerializeField] protected ChargeableItemSO m_ChargeableData = null;
    public ChargeableItemSO ChargeableData => m_ChargeableData;

    protected Collider m_Collider;
    
    protected virtual void Awake()
    {
        if (!TryGetComponent<Collider>(out m_Collider))
        {
            Debug.LogError($"Collectable {gameObject.name} is missing a Collider!");
        }
    }
    
    protected virtual void Start()
    {
        //Assert.IsNotNull(m_ItemData, $"ItemData on {name} is null!");
        //Assert.IsFalse(ItemData.CollectableType is GameEnums.CollectableTypes.Undefined, $"Collectable type of {name} is undefined!");
    }

    public abstract void MarkObject();

    public abstract void UnmarkObject();

    public GameEnums.InteractionType InteractionType => GameEnums.InteractionType.Charge;
    
    public GameObject InteractableGameObject => gameObject;
}
