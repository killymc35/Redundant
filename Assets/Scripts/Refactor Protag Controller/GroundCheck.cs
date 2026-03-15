using Unity.VisualScripting;
using UnityEngine;

public static class GroundCheck
{
    public static RaycastHit? Execute(CapsuleCollider collider, LayerMask groundLayer)
    {
        #region Variables
        Vector3 capsuleCenter = collider.transform.TransformPoint(collider.center);
        float worldRadius = collider.radius * collider.transform.lossyScale.x;
        Vector3 origin = capsuleCenter - Vector3.up * ((collider.height / 2) - worldRadius);
        #endregion
        
        #region Hit
        if (Physics.SphereCast(
                origin,
                worldRadius,
                Vector3.down,
                out RaycastHit hit,
                collider.height,
                groundLayer,
                QueryTriggerInteraction.Ignore))
        {
            return hit;
        }
        #endregion
        #region Miss
        return null;
        #endregion
    }
}
