using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float timer;
    float timePassed;

    public UnityEngine.UI.Text timerLabel;

    public List<Forniture> player1Inventory = new List<Forniture>();
    public List<Forniture> player2Inventory = new List<Forniture>();

    private Dictionary<int, float> multipliers = new Dictionary<int, float>() { { 0, 0f }, { 1, 1f }, { 2, 1.1f }, { 3, 1.2f }, { 4, 1.4f }, { 5, 1.6f }, { 6, 1.9f }, { 7, 2.5f } };

    public int Player1Score;
    public int Player2Score;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        timePassed = timer;
    }

    private void Update()
    {
        timePassed -= Time.deltaTime;

        timerLabel.text = Mathf.RoundToInt(timePassed).ToString();

        if (timePassed >= timer)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        foreach (var item in GameObject.Find("Player1").GetComponent<Player>().Inv.inventory)
        {
            player1Inventory.Add(item.fornitureInfos);
        }

        foreach (var item in GameObject.Find("Player2").GetComponent<Player>().Inv.inventory)
        {
            player1Inventory.Add(item.fornitureInfos);
        }

        Player1Score = calculatePoints(player1Inventory.ToArray());
        Player2Score = calculatePoints(player2Inventory.ToArray());

        SceneManager.LoadScene("End");
        
    }

    int calculatePoints(Forniture[] inv)
    {
        int furry = 0;
        int furryPoints = 0;
        int mosconi = 0;
        int mosconiPoints = 0;
        int meme = 0;
        int memePoint = 0;

        foreach (var item in inv)
        {
            switch (item.set)
            {
                case FornitureSet.FURRY:
                    furry++;
                    furryPoints += item.value;
                    break;
                case FornitureSet.MOSCONI:
                    mosconi++;
                    mosconiPoints += item.value;
                    break;
                case FornitureSet.MEME:
                    meme++;
                    memePoint += item.value;
                    break;
                default:
                    break;
            }
        }

        furryPoints = calculateMulty(furry, furryPoints);
        mosconiPoints = calculateMulty(mosconi, mosconiPoints);
        memePoint = calculateMulty(meme, memePoint);

        return furryPoints + mosconiPoints + memePoint;
    }

    int calculateMulty(int number, int score)
    {
        return Mathf.RoundToInt(score * multipliers[number]);
    }
}