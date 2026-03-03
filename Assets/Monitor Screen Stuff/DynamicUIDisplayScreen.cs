using UnityEngine;
using UnityEngine.Events;

public class DynamicUIDisplayScreen : MonoBehaviour
{
    [SerializeField] LayerMask RaycastMask = ~0;
    [SerializeField] float RaycastDistance = 5.0f;
    [SerializeField] UnityEvent<Vector2> OnCursorInput = new();

    void Update()
    {
#if ENABLE_LEGACY_INPUT_MANAGER
        Vector3 MousePosition = Input.mousePosition;
#else 
        Vector3 MousePosition = UnityEngine.InputSystem.Mouse.current.position.ReadValue();
#endif // ENABLE_LEGACY_INPUT_MANAGER

        // construct our ray from the mouse position 
        Ray MouseRay = Camera.main.ScreenPointToRay(MousePosition);

        // perform our raycast 
        RaycastHit HitResult;
        if (Physics.Raycast(MouseRay, out HitResult, RaycastDistance, RaycastMask, QueryTriggerInteraction.Ignore))
        {
            // ignore if not us 
            if (HitResult.collider.gameObject != gameObject) return;

            OnCursorInput.Invoke(HitResult.textureCoord);
        }
    }
}
