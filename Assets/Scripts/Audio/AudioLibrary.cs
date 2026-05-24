using UnityEngine;
using FMODUnity;

public class AudioLibrary : MonoSingleton<AudioLibrary>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
    
    [Header("Music")]
    [field: SerializeField] public EventReference BackgroundMusic { get; private set; }
    
    [Header("UI")]
    [field: SerializeField] public EventReference UiElementChange { get; private set; }
    [field: SerializeField] public EventReference UiSliderValueChange { get; private set; }
    [field: SerializeField] public EventReference UiSubmit { get; private set; }
    [field: SerializeField] public EventReference UiPanelOpen { get; private set; }
    [field: SerializeField] public EventReference UiPop { get; private set; }
    
    [Header("Player Effects")]
    [field: SerializeField] public EventReference PlayerJump { get; private set; }
    [field: SerializeField] public EventReference PlayerLands { get; private set; }
    [field: SerializeField] public EventReference PlayerFootsteps { get; private set; }
    [field: SerializeField] public EventReference PlayerShutdown { get; private set; }
    
    [Header("Battery")]
    [field: SerializeField] public EventReference BatteryCharge { get; private set; }
    [field: SerializeField] public EventReference BatteryDrain { get; private set; }
    [field: SerializeField] public EventReference BatteryWarning { get; private set; }
    [field: SerializeField] public EventReference BatteryCritical { get; private set; }
    
    [Header("Objects")]
    [field: SerializeField] public EventReference DoorOpen { get; private set; }
    [field: SerializeField] public EventReference DoorClose { get; private set; }
}
