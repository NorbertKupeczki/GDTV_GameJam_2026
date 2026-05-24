using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class AudioManager : MonoSingleton<AudioManager>
{
    private EventInstance backgroundMusic;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //backgroundMusic = RuntimeManager.CreateInstance(AudioLibrary.Instance.BackgroundMusic);
        //backgroundMusic.start();
        
        InputManager.Instance.OnUiNavigatePressed += HandleUiNavigation;
    }

    private void OnDestroy()
    {
        //backgroundMusic.stop(STOP_MODE.IMMEDIATE);
        //backgroundMusic.release();
        
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
}
