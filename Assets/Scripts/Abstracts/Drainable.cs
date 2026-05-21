using UnityEngine;
using UnityEngine.Assertions;

public abstract class Drainable : MonoBehaviour, IInteractable
{
    [SerializeField] protected DrainableItemSO m_DrainableData = null;
    public DrainableItemSO ItemData => m_DrainableData;

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

    public GameEnums.InteractionType InteractionType => GameEnums.InteractionType.Drain;
    
    public GameObject InteractableGameObject => gameObject;
}
