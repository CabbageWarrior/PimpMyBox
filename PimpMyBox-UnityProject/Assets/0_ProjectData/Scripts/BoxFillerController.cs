using System.Collections.Generic;
using UnityEngine;

public class BoxFillerController : MonoBehaviour
{
    FornitureInstance[] fornitureInstances;

    public House house1;
    public House house2;

    private void Start()
    {
        fornitureInstances = GetComponentsInChildren<FornitureInstance>(true);

        // ToDo: Add research of House components.

        if (house1 == null || house2 == null) return;

        ExecBoxFill();
    }

    private void ExecBoxFill()
    {
        for (int i = 0; i < fornitureInstances.Length; i++)
        {
            if (fornitureInstances[i].playerIndex == 1)
            {
                fornitureInstances[i].CheckPresence(house1);
            }
            else
            {
                fornitureInstances[i].CheckPresence(house2);
            }
        }
    }
}
