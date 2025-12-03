using UnityEngine;

public class ObjectiveSlot : MonoBehaviour
{
    public string requiredItemName;
    public GameObject objectToOpen; // nakama "atslega", kuru iespawnot
    public GameObject finalObjective; // pedejie varti, kam japazud

    public void TryActivate(Pickup pickup)
    {
        GameObject held = pickup.GetHeldObject();
        if (held == null) return;

        ItemID id = held.GetComponent<ItemID>();
        if (id == null) return;

        if (id.itemName == requiredItemName)
        {
            Debug.Log("Correct item inserted: " + requiredItemName);

            // Remove item from hand
            pickup.RemoveHeldObject();
            Destroy(held);

            if (objectToOpen != null)
            {
                objectToOpen.SetActive(true);

            }

            if (finalObjective != null)
            {
                Destroy(finalObjective);
            }

            //Destroy(gameObject); // ja ir nepieciesams, ka objektam japazud, piemeram, nakama atslega ir pieejama aiz si objekta
        }
    }
}
