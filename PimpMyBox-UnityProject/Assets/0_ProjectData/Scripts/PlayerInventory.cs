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
                if (inventory[i] != null)
                {
                    result--;
                }
            }

            return result;
        }
    }
    public void Drop(int index, bool playSingleDropSound = true)
    {
        if (inventory[index] != null)
        {
            SpawnManager.Instance.EnqueueObject(inventory[index]);
            inventory[index] = null;
            if (playSingleDropSound)
                AudioSingleton.PlaySound(AudioSingleton.Sound.SingleObjTrash);
            if (RemovedFromInventory != null)
                RemovedFromInventory.Invoke(index);
        }
    }

    public void DropAll()
    {
        AudioSingleton.PlaySound(AudioSingleton.Sound.MultipleObjTrash);
        for (int i = 0; i < inventory.Length; i++)
        {
            Drop(i, false);
        }
    }

    public void AddItem(FornitureGameplayObject forniture)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = forniture;
                AudioSingleton.PlaySound(AudioSingleton.Sound.PickupObject);
                break;
            }
        }
    }
}
