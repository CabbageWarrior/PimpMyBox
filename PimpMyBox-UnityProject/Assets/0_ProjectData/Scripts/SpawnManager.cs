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

    public GameInterfaceController gic;

    private void Awake()
    {
        System.Random rnd = new System.Random();

        fornitureDatabase = fornitureDatabase.OrderBy(x => rnd.Next()).ToArray();

        foreach (var item in fornitureDatabase)
        {
            spawnQueue.Enqueue(item);
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


public class GameManager : MonoBehaviour
{
    int min;
    int sec;

    public float timer;
    float timePassed;

    List<Forniture> player1 = new List<Forniture>();
    List<Forniture> player2 = new List<Forniture>();
    
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        timePassed += Time.deltaTime;

        if(timePassed >= timer)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        foreach (var item in GameObject.Find("Player1").GetComponent<Player>().Inv.inventory)
        {
            player1.Add(item.fornitureInfos);
        }

        foreach (var item in GameObject.Find("Player2").GetComponent<Player>().Inv.inventory)
        {
            player1.Add(item.fornitureInfos);
        }


    }
}