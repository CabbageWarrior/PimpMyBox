using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class House : MonoBehaviour
{
    List<FornitureGameplayObject> storedForniture = new List<FornitureGameplayObject>();
    public Player owner;

    public bool AddItem (FornitureGameplayObject item)
    {
        bool succeded = true;

        foreach (var forniture in storedForniture)
        {
            if(forniture == item || item.fornitureInfos.type == forniture.fornitureInfos.type)
            {
                succeded = false;
                break;
            }
        }

        if (succeded)
        {
            storedForniture.Add(item);
        }

        return succeded;
    }

    public bool CheckExistence(FornitureSet fornitureSet, FornitureType fornitureType)
    {
        return storedForniture.Exists(f => f.fornitureInfos.set == fornitureSet && f.fornitureInfos.type == fornitureType);
    }
}