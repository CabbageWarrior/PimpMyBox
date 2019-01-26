using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnManager : MonoBehaviour
{
    private Queue<FornitureGameplayObject> spawnQueue = new Queue<FornitureGameplayObject>();
    public FornitureGameplayObject[] fornitureDatabase;

    int currentItemNumber = 0;

    public float spawnDelay = 3f;

    public SpawnPoint[] usedSpawnPoints;

    private void Awake()
    {
        fornitureDatabase = fornitureDatabase.OrderBy(x => new System.Random()).ToArray();

        foreach (var item in fornitureDatabase)
        {
            spawnQueue.Enqueue(item);
        }       
    }

    private void Start()
    {
        while (currentItemNumber <3)
        {
            SpawnObject();
        }
    }

    IEnumerator spawnCO()
    {
        yield return new WaitForSeconds(spawnDelay);
        SpawnObject();
    }

    void SpawnObject()
    {
        bool done = false;
        var itemToSpawn = spawnQueue.Dequeue();
        while (!done)
        {
            foreach (var spawnPoint in usedSpawnPoints)
            {
                if (spawnPoint.locked == false)
                {
                    itemToSpawn.Spawn(spawnPoint.transform);
                    done = true;
                    currentItemNumber++;
                    spawnPoint.locked = true;
                    itemToSpawn.PickUp += (a) => { spawnPoint.locked = false; }; 
                    break;
                }
            }
        }
    }
}
