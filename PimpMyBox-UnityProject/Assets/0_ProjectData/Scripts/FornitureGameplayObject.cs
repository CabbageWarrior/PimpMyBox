using UnityEngine;

public class FornitureGameplayObject : MonoBehaviour
{
    public System.Action<Player> PickUp;
    public Forniture fornitureInfos;
    private SpriteRenderer sR;

    Vector2 storageLocation = new Vector2(100f, 100f);


    private void Awake()
    {
        sR = GetComponent<SpriteRenderer>();
        sR.sprite = fornitureInfos.onMapView;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>())
        {
            if (PickUp != null)
                PickUp.Invoke(other.GetComponent<Player>());

            DeSpawn();
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
