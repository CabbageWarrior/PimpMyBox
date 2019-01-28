using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameInterfaceController : MonoBehaviour
{
    [Header("Players")]
    public Player player1;
    public Player player2;

    [Header("=> Slots Player 1")]
    public Image itemA_P1;
    public Image itemB_P1;
    public Image itemX_P1;
    public Image itemY_P1;

    [Header("=> Slots Player 2")]
    public Image itemA_P2;
    public Image itemB_P2;
    public Image itemX_P2;
    public Image itemY_P2;

    [Header("Homes")]
    public House house1;
    public House house2;

    [Header("=> Slots Player 1")]
    public Image tavolo_P1;
    public Image sedia_P1;
    public Image lampada_P1;
    public Image tappeto_P1;
    public Image poster_P1;
    public Image soprammobile_P1;
    public Image trofeo_P1;

    [Header("=> Slots Player 2")]
    public Image tavolo_P2;
    public Image sedia_P2;
    public Image lampada_P2;
    public Image tappeto_P2;
    public Image poster_P2;
    public Image soprammobile_P2;
    public Image trofeo_P2;

    public void RefreshUI()
    {
        StartCoroutine(RefreshUI_Coroutine());
    }
    private IEnumerator RefreshUI_Coroutine()
    {
        yield return null; // Jumps a frame in order to refresh correctly all the infos.

        // Carts

        // Player 1
        UpdateCartButtonImage(itemA_P1, player1, 0);
        UpdateCartButtonImage(itemB_P1, player1, 1);
        UpdateCartButtonImage(itemX_P1, player1, 2);

        itemY_P1.enabled = (itemA_P1.enabled || itemB_P1.enabled || itemX_P1.enabled);

        // Player 2
        UpdateCartButtonImage(itemA_P2, player2, 0);
        UpdateCartButtonImage(itemB_P2, player2, 1);
        UpdateCartButtonImage(itemX_P2, player2, 2);

        itemY_P2.enabled = (itemA_P2.enabled || itemB_P2.enabled || itemX_P2.enabled);

        // Hauses

        // Player1
        UpdateHouseButtonImage(house1, FornitureType.CADREGA);
        UpdateHouseButtonImage(house1, FornitureType.LAMPADARIO);
        UpdateHouseButtonImage(house1, FornitureType.QUADRO);
        UpdateHouseButtonImage(house1, FornitureType.TAPPETO);
        UpdateHouseButtonImage(house1, FornitureType.TAVOLO);
        UpdateHouseButtonImage(house1, FornitureType.TROFEI);
        UpdateHouseButtonImage(house1, FornitureType.VASO);

        // Player2
        UpdateHouseButtonImage(house2, FornitureType.CADREGA);
        UpdateHouseButtonImage(house2, FornitureType.LAMPADARIO);
        UpdateHouseButtonImage(house2, FornitureType.QUADRO);
        UpdateHouseButtonImage(house2, FornitureType.TAPPETO);
        UpdateHouseButtonImage(house2, FornitureType.TAVOLO);
        UpdateHouseButtonImage(house2, FornitureType.TROFEI);
        UpdateHouseButtonImage(house2, FornitureType.VASO);
    }

    private void UpdateCartButtonImage(Image itemImageRef, Player playerRef, int itemIndex)
    {
        if (playerRef.Inv.inventory[itemIndex] == null)
        {
            itemImageRef.enabled = false;
        }
        else
        {
            itemImageRef.enabled = true;
            itemImageRef.sprite = playerRef.Inv.inventory[itemIndex].fornitureInfos.HUDView;
            itemImageRef.preserveAspect = true;
        }
    }
    private void UpdateHouseButtonImage(House houseRef, FornitureType fornitureType)
    {
        Image itemImageRef = null;
        switch (fornitureType)
        {
            case FornitureType.TAPPETO:
                itemImageRef = (houseRef.owner.playerNumber == 1 ? tappeto_P1 : tappeto_P2);
                break;
            case FornitureType.CADREGA:
                itemImageRef = (houseRef.owner.playerNumber == 1 ? sedia_P1 : sedia_P2);
                break;
            case FornitureType.TAVOLO:
                itemImageRef = (houseRef.owner.playerNumber == 1 ? tavolo_P1 : tavolo_P2);
                break;
            case FornitureType.VASO:
                itemImageRef = (houseRef.owner.playerNumber == 1 ? soprammobile_P1 : soprammobile_P2);
                break;
            case FornitureType.LAMPADARIO:
                itemImageRef = (houseRef.owner.playerNumber == 1 ? lampada_P1 : lampada_P2);
                break;
            case FornitureType.QUADRO:
                itemImageRef = (houseRef.owner.playerNumber == 1 ? poster_P1 : poster_P2);
                break;
            case FornitureType.TROFEI:
                itemImageRef = (houseRef.owner.playerNumber == 1 ? trofeo_P1 : trofeo_P2);
                break;
            default:
                break;
        }

        if (itemImageRef == null) return;

        if (!houseRef.StoredForniture.Exists(x => x.fornitureInfos.type == fornitureType))
        {
            itemImageRef.enabled = false;
        }
        else
        {
            itemImageRef.enabled = true;
            itemImageRef.sprite = houseRef.StoredForniture.First(x => x.fornitureInfos.type == fornitureType).fornitureInfos.HUDView;
            itemImageRef.preserveAspect = true;
        }
    }
}
