using UnityEngine;
using UnityEngine.InputSystem;

public class Pickup : MonoBehaviour
{
    public Transform handTransform;
    public float interactRange = 3f;
    public float sphereRadius = 2f;

    private GameObject heldObject = null;
    bool isPickedUp = false;

    void Update()
    {
        if (Keyboard.current[Key.E].wasPressedThisFrame)
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        Vector3 rayOrigin = Camera.main.transform.position;
        Vector3 rayDirection = Camera.main.transform.forward;

        RaycastHit hit;
        if (Physics.SphereCast(rayOrigin, sphereRadius, rayDirection, out hit, interactRange))
        {
            // megina aktivizet objektu
            ObjectiveSlot slot = hit.collider.GetComponent<ObjectiveSlot>();
            if (slot != null)
            {
                slot.TryActivate(this);
                return;
            }

            // citadak megina pacelt objektu
            if (!isPickedUp && hit.collider.CompareTag("Pickable"))
            {
                PickupObject(hit.collider.gameObject);
            }
        }
    }

    void PickupObject(GameObject obj)
    {
        heldObject = obj;

        heldObject.transform.SetParent(handTransform);
        heldObject.transform.localPosition = Vector3.zero;
        heldObject.transform.localRotation = Quaternion.identity;

        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = true;

        isPickedUp = true;
    }

    public void Drop()
    {
        if (heldObject != null)
        {
            isPickedUp = false;

            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            if (rb != null)
                rb.isKinematic = false;

            heldObject.transform.SetParent(null);
            heldObject = null;
        }
    }

    public GameObject GetHeldObject() => heldObject;
    public void RemoveHeldObject() => Drop();
}
