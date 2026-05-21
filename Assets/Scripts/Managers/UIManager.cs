using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private TMP_Text m_InteractionText;
    
    [Header("Pause Menu")]
    [SerializeField] private Transform m_PauseMenu;
    [SerializeField] private Button m_ResumeButton;
    [SerializeField] private Button m_SettingsButton;
    [SerializeField] private Button m_MainMenuButton;
    
    [Header("Settings Panel")]
    [SerializeField] private SettingsPanel m_SettingsPanel;

    private const string INTERACTION_PICKUP = "Pick up";
    private const string INTERACTION_DRAIN = "Drain";
    private const string INTERACTION_CHARGE = "Charge";
    private const string INTERACTION_INSERT = "Insert";
    private const string INTERACTION_USE = "USE";
    
    protected override void Awake()
    {
        base.Awake();
        
        ToggleInteractionText(false, GameEnums.InteractionType.None);
    }

    private void Start()
    {
        PlayerManager.Instance.OnInteractableSelected += ToggleInteractionText;
        InputManager.Instance.OnMenuPressed += HandleMenuButtonPressed;
        
        // Button onClick subscriptions
        m_ResumeButton.onClick.AddListener(HandleResumeButton);
        m_SettingsButton.onClick.AddListener(HandleSettingsButton);
        m_MainMenuButton.onClick.AddListener(HandleMainMenuButton);
        
        m_SettingsPanel.OnSettingsPanelClose += HandleSettingsPanelClose;
        
        m_PauseMenu.gameObject.SetActive(false);
        m_SettingsPanel.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        PlayerManager.Instance.OnInteractableSelected -= ToggleInteractionText;
        InputManager.Instance.OnMenuPressed -= HandleMenuButtonPressed;
        
        m_ResumeButton.onClick.RemoveAllListeners();
        m_SettingsButton.onClick.RemoveAllListeners();
        m_MainMenuButton.onClick.RemoveAllListeners();
        
        m_SettingsPanel.OnSettingsPanelClose -= HandleSettingsPanelClose;
    }
    
    private void ToggleInteractionText(bool toggle, GameEnums.InteractionType interactionType)
    {
        m_InteractionText.gameObject.SetActive(toggle);

        m_InteractionText.text = interactionType switch
        {
            GameEnums.InteractionType.None => "",
            GameEnums.InteractionType.Pickup => INTERACTION_PICKUP,
            GameEnums.InteractionType.Drain  => INTERACTION_DRAIN,
            GameEnums.InteractionType.Charge => INTERACTION_CHARGE,
            GameEnums.InteractionType.Insert => INTERACTION_INSERT,
            GameEnums.InteractionType.Use => INTERACTION_USE,
            _ => throw new ArgumentOutOfRangeException(nameof(interactionType), interactionType, null)
        };
    }

    private void HandleMenuButtonPressed()
    {
        TogglePauseGame(true);
    }

    private void TogglePauseGame(bool pause)
    {
        // Stop/resume time
        Time.timeScale = pause ? 0 : 1;
        
        // Switch Action maps
        InputManager.Instance.SwitchToInputMap(pause? InputManager.InputMaps.UI : InputManager.InputMaps.Game);

        // Show UI element
        m_PauseMenu.gameObject.SetActive(pause);
        
        if (pause)
        {
            m_ResumeButton.Select();
        }
    }
    
    private void HandleResumeButton()
    {
        TogglePauseGame(false);
    }

    private void HandleSettingsButton()
    {
        m_PauseMenu.gameObject.SetActive(false);
        ToggleSettingsPanel(true);
    }

    private void HandleMainMenuButton()
    {
        Loader.LoadScene(Loader.Scenes.MainMenu);
    }

    private void HandleSettingsPanelClose()
    {
        ToggleSettingsPanel(false);
    }
    
    private void ToggleSettingsPanel(bool toggle)
    {
        m_SettingsPanel.gameObject.SetActive(toggle);

        if (toggle) { return; }
        
        m_PauseMenu.gameObject.SetActive(true);
        m_SettingsButton.Select();
    }
}
