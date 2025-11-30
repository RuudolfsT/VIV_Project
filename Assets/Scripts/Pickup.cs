using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class Pickup : MonoBehaviour
{
    public Transform handTransform; // Drag your hand object here
    public float pickupRange = 3f;  // Max distance to pick up

    private GameObject heldObject = null;
    bool isPickedUp = false;

    void Update()
    {
        if (Keyboard.current[Key.E].wasPressedThisFrame)
        {
            if (!isPickedUp)
            {
                TryPickup();
            }
            else
            {
                //Drop(); // nez kas te notiek, bet ja spamo E un griezas rinki tad objekts var pazust
            }



            //if (heldObject == null)
            //    TryPickup();
            //else
            //    Drop();
        }
    }
    void TryPickup()
    {
        float sphereRadius = 1f; // adjust to make aiming easier
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = transform.forward;

        RaycastHit hit;
        if (Physics.SphereCast(rayOrigin, sphereRadius, rayDirection, out hit, pickupRange))
        {
            if (hit.collider != null && hit.collider.CompareTag("Pickable"))
            {
                heldObject = hit.collider.gameObject;

                // Parent it to the hand and reset position/rotation
                heldObject.transform.SetParent(handTransform);
                heldObject.transform.localPosition = Vector3.zero;
                heldObject.transform.localRotation = Quaternion.identity;

                // Make it kinematic so it doesn't fall
                Rigidbody rb = heldObject.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.isKinematic = true;

                isPickedUp = true;
            }
        }
    }

    void Drop()
    {
        if (heldObject != null)
        {
            isPickedUp = false;
            // Restore physics
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            if (rb != null)
                rb.isKinematic = false;

            // Detach from hand
            heldObject.transform.SetParent(null);
            heldObject = null;
        }
    }
}
