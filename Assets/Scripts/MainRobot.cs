using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainRobot : MonoBehaviour
{
    [SerializeField] private List<MissingRobotPartStation> m_MissingParts;
    [SerializeField] private Animator m_Animator;
    [SerializeField] private SkinnedMeshRenderer m_MainBodyRenderer;

    [Header("Door to open once completed")]
    [SerializeField] private SlidingDoubleDoor m_FinalDoor;

    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
    private readonly Color m_EmissionColor = Color.white;
    
    private void Start()
    {
        foreach (var part in m_MissingParts)
        {
            part.OnPartDeposited += HandlePartDeposited;
        }
    }

    private void HandlePartDeposited(ItemDataSO item)
    {
        m_MissingParts.Remove(m_MissingParts.Find((x) => x.DepositableItems == item));

        if (m_MissingParts.Count > 0) { return; }
        
        m_Animator.enabled = true;
        StartCoroutine(SetGlowIntensityToMax());
    }

    private IEnumerator SetGlowIntensityToMax()
    {
        var delay = new WaitForSeconds(0.05f);
        var glowIntensity = 0.0f;

        while (glowIntensity < 30.0f)
        {
            glowIntensity += 0.5f;
            m_MainBodyRenderer.material.SetVector(EmissionColor,m_EmissionColor * glowIntensity );
            yield return delay;
        }
        
        yield return null;
        
        m_FinalDoor?.OpenDoor();
    }
}
