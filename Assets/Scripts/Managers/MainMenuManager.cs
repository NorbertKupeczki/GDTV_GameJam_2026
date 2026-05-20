using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button m_StartGameButton;
    [SerializeField] private Button m_SettingsButton;
    [SerializeField] private Button m_QuitButton;

    private void Awake()
    {
        m_StartGameButton.Select();
        
        m_StartGameButton.onClick.AddListener(HandleStartButtonPressed);
        m_SettingsButton.onClick.AddListener(HandleSettingsButtonPressed);
        m_QuitButton.onClick.AddListener(HandleQuitButtonPressed);
    }

    private void OnDestroy()
    {
        m_StartGameButton.onClick.RemoveAllListeners();
        m_SettingsButton.onClick.RemoveAllListeners();
        m_QuitButton.onClick.RemoveAllListeners();
    }

    private void HandleStartButtonPressed()
    {
        Loader.LoadScene((Loader.Scenes)SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void HandleSettingsButtonPressed()
    {
        Debug.Log("Settings");
    }

    private void HandleQuitButtonPressed()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
