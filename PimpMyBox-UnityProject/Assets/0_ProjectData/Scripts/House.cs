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

        foreach (var forniture in storedForniture)
        {
            if(forniture == item)
            {
                succeded = false;
                break;
            }

            if (item.fornitureInfos.type == forniture.fornitureInfos.type)
            {
                storedForniture.Remove(forniture);               
            }
        }

        if (succeded)
        {
            storedForniture.Add(item);
        }

        return succeded;
    }
}