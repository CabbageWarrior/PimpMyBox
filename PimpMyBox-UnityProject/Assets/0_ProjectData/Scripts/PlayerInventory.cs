using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public System.Action<int> RemovedFromInventory;
    public FornitureGameplayObject[] inventory = new FornitureGameplayObject[3];
    public int FreeSlots
    {
        get
        {
            int result = 3;
            for (int i = 0; i < inventory.Length; i++)
            {
                if(inventory[i] != null)
                {
                    result--;
                }
            }

            return result;
        }
    }
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

    public void AddItem(FornitureGameplayObject forniture)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if(inventory[i] == null)
            {
                inventory[i] = forniture;
                break;
            }
        }
    }
}
