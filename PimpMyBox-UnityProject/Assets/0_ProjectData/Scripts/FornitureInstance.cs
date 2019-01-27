using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FornitureInstance : MonoBehaviour
{
    public FornitureType fornitureType;
    public FornitureSet fornitureSet;
    public int playerIndex;

    public void CheckPresence(House house)
    {
        bool exists = house.CheckExistence(fornitureSet, fornitureType);
        gameObject.SetActive(exists);

        if (fornitureType == FornitureType.TAVOLO)
        {
            transform.parent.GetChild(0).gameObject.SetActive(false);
        }
    }
}
