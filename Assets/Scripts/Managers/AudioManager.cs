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
    }

    private void Start()
    {
        backgroundMusic = RuntimeManager.CreateInstance(AudioLibrary.Instance.backgroundMusic);
        backgroundMusic.start();
    }

    private void OnDestroy()
    {
        backgroundMusic.stop(STOP_MODE.IMMEDIATE);
        backgroundMusic.release();
    }

    public void PlayOneShotAudio(EventReference eventReference, Vector3 position)
    {
        RuntimeManager.PlayOneShot(eventReference, position);
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        return RuntimeManager.CreateInstance(eventReference);
    }
}
