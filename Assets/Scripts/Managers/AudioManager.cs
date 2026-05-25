using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class AudioManager : MonoSingleton<AudioManager>
{
    private EventInstance m_BackgroundMusic;
    
    private Bus m_MasterBus;
    private Bus m_EffectsBus;
    private Bus m_MusicBus;
    
    protected override void Awake()
    {
        base.Awake();
        CreateAudioBuses();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        m_BackgroundMusic = RuntimeManager.CreateInstance(AudioLibrary.Instance.BackgroundMusic);
        m_BackgroundMusic.start();
        
        InputManager.Instance.OnUiNavigatePressed += HandleUiNavigation;
    }

    private void OnDestroy()
    {
        m_BackgroundMusic.stop(STOP_MODE.IMMEDIATE);
        m_BackgroundMusic.release();
        
        InputManager.Instance.OnUiNavigatePressed -= HandleUiNavigation;
    }

    public void PlayOneShotAudio(EventReference eventReference, Vector3 position)
    {
        RuntimeManager.PlayOneShot(eventReference, position);
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        return RuntimeManager.CreateInstance(eventReference);
    }

    private void HandleUiNavigation(Vector2 navDir)
    {
        if (Mathf.Abs(navDir.x) > float.Epsilon) { return; }
        PlayOneShotAudio(AudioLibrary.Instance.UiElementChange, Camera.main.transform.position );
    }
    
    private void CreateAudioBuses()
    {
        //Debug.Log("Audio buses loading...");
        m_MasterBus = RuntimeManager.GetBus("bus:/");
        m_MusicBus = RuntimeManager.GetBus("bus:/Music");
        m_EffectsBus = RuntimeManager.GetBus("bus:/SFX");
    }
    
    /// <summary>
    ///     Sets the music volume (0.0 - 1.0)
    /// </summary>
    /// <param name="volume"></param>
    public void SetMusicVolume(float volumeNormalised)
    {
        m_MusicBus.setVolume(volumeNormalised);
    }

    /// <summary>
    ///     Sets the effects volume (0.0f - 1.0f)
    /// </summary>
    /// <param name="volume"></param>
    public void SetEffectsVolume(float volumeNormalised)
    {
        m_EffectsBus.setVolume(volumeNormalised);
    }
}
