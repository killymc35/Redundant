using UnityEngine;

public static class CollideAndSlide
{
    #region Variables
    public const int MaxDepth = 3;
    private const float FloatingPointErrorCheck = 0.001f;
    private const float LeewayFraction = 0.95f;
    private const float MaxSlopeAngle = 55f;
    #endregion
    
    public static Vector3 Execute(CapsuleCollider collider, LayerMask collisionLayers, Vector3 velocity, Vector3 position, Vector3 targetDirection, int depth)
    {
        #region Base Case

        if (depth >= MaxDepth)
        {
            return Vector3.zero;
        }

        #endregion
        #region Logic

        #region GetColliderInfo

        var capsuleRadius = collider.radius;
        var capsuleHeight = collider.height;
        var capsulePointFromCenter = (capsuleHeight / 2) - capsuleRadius;

        #endregion

        float distance = velocity.magnitude;

        #region FloatingPointErrorCheck
        if (distance < FloatingPointErrorCheck)
        {
            return Vector3.zero;
        }
        #endregion
        #region CreateCapsuleCastParameters
        RaycastHit hit;

        Vector3 point1 = new Vector3(position.x, position.y + capsulePointFromCenter, position.z);
        Vector3 point2 = new Vector3(position.x, position.y - capsulePointFromCenter, position.z);
        #endregion

        if (Physics.CapsuleCast(
                point1,
                point2,
                capsuleRadius * LeewayFraction,
                velocity.normalized,
                out hit,
                distance,
                collisionLayers,
                QueryTriggerInteraction.Ignore))
        {
            Vector3 snapToSurface = velocity.normalized * Mathf.Max(0, hit.distance - FloatingPointErrorCheck);
            Vector3 remainder = velocity - snapToSurface;
            float angle = Vector3.Angle(Vector3.up, hit.normal);

            float scale;

            if (angle <= MaxSlopeAngle)
            {
                scale = 1;
            }
            else
            {
                scale = 1 - Vector3.Dot(
                    new Vector3(hit.normal.x, 0, hit.normal.z).normalized,
                    -new Vector3(targetDirection.x, 0, targetDirection.z).normalized);
            }

            remainder = Vector3.ProjectOnPlane(remainder, hit.normal) * scale;

            return snapToSurface + Execute(
                collider,
                collisionLayers,
                remainder,
                position + snapToSurface,
                targetDirection,
                depth + 1);
        }

        #endregion
        #region Default Case

        return velocity;

        #endregion
    }
}
