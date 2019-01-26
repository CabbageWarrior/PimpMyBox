using UnityEngine;
using System.Collections.Generic;

public class House : MonoBehaviour
{
    List<FornitureGameplayObject> storedForniture = new List<FornitureGameplayObject>();

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
}