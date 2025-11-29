using UnityEngine;
using UnityEngine.InputSystem;

public class MouseMovement_New : MonoBehaviour
{
    public float mouseSensitivity = 500f;

    private InputAction lookAction;

    float xRotation = 0f;
    float yRotation = 0f;

    private void OnEnable()
    {
        // Create and enable input action
        lookAction = new InputAction(type: InputActionType.Value, binding: "<Mouse>/delta");
        lookAction.Enable();
    }

    private void OnDisable()
    {
        lookAction.Disable();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector2 mouseDelta = lookAction.ReadValue<Vector2>() * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseDelta.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += mouseDelta.x;

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
