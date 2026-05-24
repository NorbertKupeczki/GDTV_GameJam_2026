using UnityEngine;
using FMODUnity;

public class AudioLibrary : MonoBehaviour
{
    [Header("Music")]
    [field: SerializeField] public EventReference backgroundMusic { get; private set; }
    
    [Header("Sound Effects")]
    [field: SerializeField] public EventReference playerJump { get; private set; }
    [field: SerializeField] public EventReference playerLands { get; private set; }
    [field: SerializeField] public EventReference playerFootsteps { get; private set; }
    [field: SerializeField] public EventReference enemyMovement { get; private set; }
    
    
    public static AudioLibrary Instance {get; private set;}

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("AudioLibrary has already been initialized!");
            Destroy(gameObject);
        }
    }
}
