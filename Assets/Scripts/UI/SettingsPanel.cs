using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    public event Action OnSettingsPanelClose;
    
    [SerializeField] private VolumeSlider m_MusicVolumeSlider;
    [SerializeField] private VolumeSlider m_EffectsVolumeSlider;
    [SerializeField] private Button m_BackButton;

    private void Awake()
    {
        m_MusicVolumeSlider.OnSliderValueChanged += HandleMusicSliderValueChanged;
        m_EffectsVolumeSlider.OnSliderValueChanged += HandleEffectsVolumeSliderValueChanged;
        m_BackButton.onClick.AddListener(HandleBackButtonPressed);
    }

    private void OnEnable()
    {
        m_BackButton.Select();
    }

    private void OnDestroy()
    {
        m_MusicVolumeSlider.OnSliderValueChanged -= HandleMusicSliderValueChanged;
        m_EffectsVolumeSlider.OnSliderValueChanged -= HandleEffectsVolumeSliderValueChanged;
        m_BackButton.onClick.RemoveListener(HandleBackButtonPressed);
    }

    private void HandleMusicSliderValueChanged(float value)
    {
        // Logic to handle music volume slider changes
    }

    private void HandleEffectsVolumeSliderValueChanged(float value)
    {
        // Logic to handle effects volume slider changes
    }
    
    private void HandleBackButtonPressed()
    {
        OnSettingsPanelClose?.Invoke();
    }
}
