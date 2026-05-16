using UnityEngine;

public interface ICollectable
{
    public GameEnums.CollectableTypes CollectableType { get; }
    public void Collect(Transform parent);
}
