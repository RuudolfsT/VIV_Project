using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public string currentItem = "";

    public bool HasItem(string itemName)
    {
        return currentItem == itemName;
    }

    public void PickItem(string itemName)
    {
        currentItem = itemName;
        Debug.Log("Picked up: " + itemName);
    }

    public void ClearItem()
    {
        currentItem = "";
    }
}