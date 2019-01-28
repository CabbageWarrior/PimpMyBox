using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxFillerController : MonoBehaviour
{
    FornitureInstance[] fornitureInstances;

    public Text player1Score;
    public Text player2Score;

    public GameObject P1WinCard;
    public GameObject P2WinCard;

    private GameManager gameManager;

    private void Start()
    {
        fornitureInstances = GetComponentsInChildren<FornitureInstance>(true);

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        player1Score.text = gameManager.Player1Score.ToString();
        player2Score.text = gameManager.Player2Score.ToString();

        if (gameManager.Player1Score > gameManager.Player2Score)
            P1WinCard.SetActive(true);

        if (gameManager.Player2Score > gameManager.Player1Score)
            P2WinCard.SetActive(true);

        ExecBoxFill();
    }

    private void ExecBoxFill()
    {
        for (int i = 0; i < fornitureInstances.Length; i++)
        {
            if (fornitureInstances[i].playerIndex == 1)
            {
                fornitureInstances[i].CheckPresence(gameManager.player1Inventory);
            }
            else
            {
                fornitureInstances[i].CheckPresence(gameManager.player2Inventory);
            }
        }
    }
}
