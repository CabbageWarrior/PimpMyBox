using UnityEngine;

public class FornitureGameplayObject : MonoBehaviour
{
    public System.Action<Player> PickUp;
    public Forniture fornitureInfos;
    private SpriteRenderer sR;
    Vector2 storageLocation = new Vector2(100f, 100f);

    public bool isSchifo = false;

    private void Awake()
    {
        sR = GetComponent<SpriteRenderer>();
        sR.sprite = fornitureInfos.onMapView;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other is BoxCollider2D)
        {
            var player = other.GetComponent<Player>();

            if (player != null && player.Inv.FreeSlots > 0)
            {
                Debug.Log(this.name);
                if (PickUp != null)
                    PickUp.Invoke(other.GetComponent<Player>());
                player.Inv.AddItem(this);
                DeSpawn();
            }
        }
    }


    public void Spawn(Transform where)
    {
        this.transform.position = where.transform.position;
    }

    private void DeSpawn()
    {
        this.transform.position = storageLocation;
    }

}
