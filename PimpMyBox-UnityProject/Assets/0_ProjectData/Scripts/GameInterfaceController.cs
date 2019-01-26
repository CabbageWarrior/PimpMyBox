using UnityEngine;
using UnityEngine.UI;

public class GameInterfaceController : MonoBehaviour
{
    [Header("Players")]
    public Player player1;
    public Player player2;

    [Header("Buttons Player 1")]
    public Image itemA_P1;
    public Image itemB_P1;
    public Image itemX_P1;
    public Image itemY_P1;

    [Header("Buttons Player 2")]
    public Image itemA_P2;
    public Image itemB_P2;
    public Image itemX_P2;
    public Image itemY_P2;

    public void RefreshUI()
    {
        // Player 1
        UpdateButtonImage(itemA_P1, player1, 0);
        UpdateButtonImage(itemB_P1, player1, 1);
        UpdateButtonImage(itemX_P1, player1, 2);

        itemY_P1.enabled = (itemA_P1.enabled || itemB_P1.enabled || itemX_P1.enabled);

        // Player 2
        UpdateButtonImage(itemA_P2, player2, 0);
        UpdateButtonImage(itemB_P2, player2, 1);
        UpdateButtonImage(itemX_P2, player2, 2);

        itemY_P2.enabled = (itemA_P2.enabled || itemB_P2.enabled || itemX_P2.enabled);
    }

    private void UpdateButtonImage(Image itemImageRef, Player playerRef, int itemIndex)
    {
        if (playerRef.Inv.inventory[itemIndex] == null)
        {
            itemImageRef.enabled = false;
        }
        else
        {
            itemImageRef.enabled = true;
            itemImageRef.sprite = playerRef.Inv.inventory[itemIndex].fornitureInfos.HUDView;
        }
    }
}
