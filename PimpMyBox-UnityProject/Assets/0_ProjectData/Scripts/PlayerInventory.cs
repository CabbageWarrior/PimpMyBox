using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public System.Action<int> RemovedFromInventory;
    public FornitureGameplayObject[] inventory = new FornitureGameplayObject[3];

    public void Drop(int index)
    {
        inventory[index] = null;
        if (RemovedFromInventory != null)
            RemovedFromInventory.Invoke(index);
    }

    public void DropAll()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            Drop(i);
        }
    }
}
