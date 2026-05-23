using UnityEngine;

public class CanvasAnchor : MonoBehaviour
{
    [Header("Follow Target")]
    [Tooltip("The transform this canvas will follow. Leave empty to use transform.parent.")]
    public Transform target;
 
    [Header("Position Offset")]
    [Tooltip("Local-space offset applied to the canvas position relative to the target.")]
    public Vector3 positionOffset = Vector3.zero;
 
    [Header("Rotation Offset")]
    [Tooltip("Euler angle offset applied to the canvas rotation relative to the target.")]
    public Vector3 rotationOffset = Vector3.zero;
 
    [Header("Options")]
    [Tooltip("Follow position of the target.")]
    public bool followPosition = true;
 
    [Tooltip("Follow rotation of the target.")]
    public bool followRotation = true;
 
    [Tooltip("Use LateUpdate to ensure the canvas moves after physics/animations settle.")]
    public bool useLateUpdate = true;
 
    // -------------------------------------------------------------------------
 
    private Canvas _canvas;
    private RectTransform _rectTransform;
 
    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _rectTransform = GetComponent<RectTransform>();
 
        if (_canvas.renderMode != RenderMode.WorldSpace)
        {
            Debug.LogWarning($"[FollowParentCanvas] Canvas on '{gameObject.name}' is not set to World Space. " +
                             "Switching automatically.", this);
            _canvas.renderMode = RenderMode.WorldSpace;
        }
 
        // Fall back to the transform's parent if no explicit target is assigned.
        if (target == null && transform.parent != null)
        {
            target = transform.parent;
        }
 
        if (target == null)
        {
            Debug.LogWarning($"[FollowParentCanvas] No target assigned and no parent found on '{gameObject.name}'. " +
                             "The canvas will not move.", this);
        }
    }
 
    private void Update()
    {
        if (!useLateUpdate)
            Sync();
    }
 
    private void LateUpdate()
    {
        if (useLateUpdate)
            Sync();
    }
 
    /// <summary>
    /// Syncs the canvas world position and rotation to match the target.
    /// </summary>
    private void Sync()
    {
        if (!target) return;
 
        if (followPosition)
        {
            // Apply the position offset in the target's local space.
            var targetVector = new Vector3(target.position.x, target.position.y, target.position.z);
            Debug.Log(targetVector);
            _rectTransform.position = targetVector;
        }
 
        if (followRotation)
        {
            // Apply the rotation offset on top of the target's world rotation.
            transform.rotation = target.rotation * Quaternion.Euler(rotationOffset);
        }
    }
 
    /// <summary>
    /// Convenience method to change the follow target at runtime.
    /// </summary>
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
