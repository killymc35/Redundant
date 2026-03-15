using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class InputHandler : MonoBehaviour
{
    public void ReceiveMovementInput(InputAction.CallbackContext input)
    {
        if (input.started) PlayerBehaviour.Instance.Begin<Walking>();
        if (input.canceled)
        {
            PlayerBehaviour.Instance.End<Walking>();
            return;
        }
        
        Walking.SyncInput(input.ReadValue<Vector2>());
    }

    public void ReceiveLookInput(InputAction.CallbackContext input)
    {
        if (input.started) PlayerBehaviour.Instance.Begin<Looking>();
        if (input.canceled) 
        {
            PlayerBehaviour.Instance.End<Looking>();
            return;
        }
        Looking.SyncInput(input.ReadValue<Vector2>());
    }

    public void ReceiveInteractInput(InputAction.CallbackContext input)
    {
        float interactionRange = 5;

        Ray centerRay = Camera.main.ScreenPointToRay(new Vector3(
            Screen.width / 2,
            Screen.height / 2,
            0f));

        if (Physics.Raycast(centerRay, out RaycastHit hitInfo , interactionRange))
        {
            if (hitInfo.collider.TryGetComponent<InteractionPoint>(out InteractionPoint interaction))
            {
                interaction.Interact();
            }
        }
    }
}
