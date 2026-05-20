using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public event Action<float> OnSliderValueChanged;
    
    [SerializeField] private Slider m_Slider;
    [SerializeField] private TMP_Text m_SliderValueText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        m_Slider.onValueChanged.AddListener(UpdateSliderValueText);
        UpdateSliderValueText(m_Slider.value);
    }

    private void OnDestroy()
    {
        m_Slider.onValueChanged.RemoveListener(UpdateSliderValueText);
    }

    private void UpdateSliderValueText(float value)
    {
        m_SliderValueText.text = Mathf.RoundToInt(value).ToString();
        OnSliderValueChanged?.Invoke(value);
    }
}
