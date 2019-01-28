using UnityEngine;
using System.Collections.Generic;

public class House : MonoBehaviour
{
    List<FornitureGameplayObject> storedForniture = new List<FornitureGameplayObject>();
    public Player owner;

    public List<FornitureGameplayObject> StoredForniture
    {
        get
        {
            return storedForniture;
        }
    }

    public bool AddItem (FornitureGameplayObject item)
    {
        bool succeded = true;
        FornitureGameplayObject objToSwap = null;
        foreach (var forniture in storedForniture)
        {
            if(forniture == item)
            {
                succeded = false;
                break;
            }

            if (item.fornitureInfos.type == forniture.fornitureInfos.type)
            {
                objToSwap = forniture;
            }
        }

        if (objToSwap != null)
        {
            storedForniture.Remove(objToSwap);
            SpawnManager.Instance.EnqueueObject(objToSwap);
        }

        if (succeded)
        {
            storedForniture.Add(item);
        }

        return succeded;
    }

    public bool RemoveRandomItem()
    {
        bool success = false;

        int rnd = Random.Range(0, storedForniture.Count);

        if (storedForniture.Count > 0)
        {
            storedForniture.RemoveAt(rnd);

            FindObjectOfType<GameInterfaceController>().RefreshUI();
            GetComponentInChildren<Animation>().Play();
            success = true;
        }

        return success;
    }
}
