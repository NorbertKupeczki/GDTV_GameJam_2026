using UnityEngine;

public class Battery : MonoBehaviour, ICollectable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Collect()
    {
        Debug.Log("Battery collected");
    }
}
