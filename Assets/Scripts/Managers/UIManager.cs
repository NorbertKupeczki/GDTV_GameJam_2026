using TMPro;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private TMP_Text m_InteractionText;

    protected override void Awake()
    {
        base.Awake();
        
        ToggleInteractionText(false);
    }

    private void Start()
    {
        PlayerManager.Instance.OnInteractableSelected += ToggleInteractionText;
    }

    private void OnDestroy()
    {
        PlayerManager.Instance.OnInteractableSelected -= ToggleInteractionText;
    }
    
    private void ToggleInteractionText(bool toggle)
    {
        if (m_InteractionText.gameObject.activeSelf == toggle) { return; }
        
        m_InteractionText.gameObject.SetActive(toggle);
    }
}
