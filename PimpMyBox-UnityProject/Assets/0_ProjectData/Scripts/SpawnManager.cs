using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager instance = null;

    public static SpawnManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new SpawnManager();
            }
            return instance;
        }
    }

    private Queue<FornitureGameplayObject> spawnQueue = new Queue<FornitureGameplayObject>();
    public FornitureGameplayObject[] fornitureDatabase;

    int currentItemNumber = 0;

    public float spawnDelay = 3f;

    public SpawnPoint[] usedSpawnPoints;

    public GameInterfaceController gic;

    private void Awake()
    {
        System.Random rnd = new System.Random();

        fornitureDatabase = fornitureDatabase.OrderBy(x => rnd.Next()).ToArray();

        foreach (var item in fornitureDatabase)
        {
            EnqueueObject(item);
            item.PickUp += (a) => { currentItemNumber--; SpawnNewObject(); gic.RefreshUI(); };
        }
    }

    private void Start()
    {
        while (currentItemNumber < 3)
        {
            SpawnObject();
        }
    }

    public void SpawnNewObject()
    {
        StartCoroutine(spawnCO());
    }

    public void EnqueueObject(FornitureGameplayObject item)
    {
        spawnQueue.Enqueue(item);
    }

    IEnumerator spawnCO()
    {
        yield return new WaitForSeconds(spawnDelay);
        if (currentItemNumber < 3)
            SpawnObject();
        yield return null;
    }

    void SpawnObject()
    {
        Debug.Log("asd");
        bool done = false;

        if (spawnQueue.Count == 0)
        {
            Debug.Log("No new objects available for spawn!");
            return;
        }

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
