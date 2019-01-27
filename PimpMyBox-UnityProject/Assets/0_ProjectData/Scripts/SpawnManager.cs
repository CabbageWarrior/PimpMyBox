using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager instance = null;

    public Player[] players;

    public GameObject[] Beers;

    public float beerSpawnTimer = 20f;
    float timer;
    bool isTimerActive = true;

    public static SpawnManager Instance
    {
        get
        {
            if (instance == null)
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


    private void Update()
    {
        if (isTimerActive)
        {
            
            timer += Time.deltaTime;

            if(timer > beerSpawnTimer)
            {
                var rnd = Random.Range(0, 2);

                Beers[rnd].SetActive(true);
                isTimerActive = false;
                timer = 0;
            }
        }

    }

    private void Start()
    {
        while (currentItemNumber < 3)
        {
            SpawnObject();
        }

        foreach (var item in players)
        {
            item.onBeerTaken += BeerIscription;
        }
    }

    void BeerIscription(GameObject beer)
    {
        beer.SetActive(false);
        isTimerActive = true;
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
