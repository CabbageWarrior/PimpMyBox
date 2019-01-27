using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FornitureInstance : MonoBehaviour
{
    public FornitureType fornitureType;
    public FornitureSet fornitureSet;
    public int playerIndex;

    public void CheckPresence(List<Forniture> fornitures)
    {
        bool exists = false;

        foreach (Forniture obj in fornitures)
        {
            if (obj.set == fornitureSet && obj.type == fornitureType)
            {
                exists = true;
                break;
            }
        }

        gameObject.SetActive(exists);

        if (exists && fornitureType == FornitureType.TAVOLO)
        {
            transform.parent.GetChild(0).gameObject.SetActive(false);
        }
    }
}
