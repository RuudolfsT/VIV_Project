using UnityEngine;
using System.Collections.Generic;
public class ObjectiveSlot : MonoBehaviour
{
    public string requiredItemName;
    public bool changePoseOnCorrectItem;
    public Transform objectToMove;
    public Transform openPose;

    public List<GameObject> objectsToOpen = new List<GameObject>(); // nakamie objectives, ko iespawnot
    public List<GameObject> objectsToHide = new List<GameObject>(); // // objective, kam butu japazud
    public bool IsFinalObjective = false;
    public void TryActivate(Pickup pickup)
    {
        GameObject held = null;
        ItemID id = null;
        if (!IsFinalObjective)
        {
            held = pickup.GetHeldObject();
            if (held == null) return;

            id = held.GetComponent<ItemID>();
            if (id == null) return;


            if (id.itemName == requiredItemName)
            {
                // Remove item from hand
                pickup.RemoveHeldObject(); 
                Destroy(held);

                foreach (GameObject obj in objectsToOpen)
                {
                    if (obj != null)
                        obj.SetActive(true);
                }

                foreach (GameObject obj in objectsToHide)
                {
                    if (obj != null)
                        Destroy(obj);
                }

                // durvis atveras
                if (changePoseOnCorrectItem && objectToMove && openPose)
                {
                    objectToMove.SetPositionAndRotation(
                        openPose.position,
                        openPose.rotation
                    );
                }

                //Destroy(gameObject); // ja ir nepieciesams, ka objektam japazud, piemeram, nakama atslega ir pieejama aiz si objekta
            }
        }
        else
        {
            foreach (GameObject obj in objectsToOpen)
            {
                if (obj != null)
                    obj.SetActive(true);
            }

            foreach (GameObject obj in objectsToHide)
            {
                if (obj != null)
                    Destroy(obj);
            }
        }
    }
}
