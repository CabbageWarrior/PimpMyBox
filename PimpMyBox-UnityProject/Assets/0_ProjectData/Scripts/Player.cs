using UnityEngine;

public class Player : MonoBehaviour
{
    public SpriteRenderer[] spriteRenderers;
    [Space]
    public int playerNumber = 1;

    public float movementSpeedMultiplier = 1f;
    public float deadMovementAxis = .19f;
    public float maxSpeed = 10f;

    public float currentSpeed;

    private Rigidbody2D rb2D;

    private PlayerInventory inv;
    public PlayerInventory Inv { get { return inv; } }

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        inv = GetComponent<PlayerInventory>();

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].gameObject.SetActive(i == playerNumber - 1);
        }
    }

    private void Update()
    {
        float xAxis = Input.GetAxis("Horizontal-P" + playerNumber.ToString());
        float yAxis = Input.GetAxis("Vertical-P" + playerNumber.ToString());

        if (Mathf.Abs(xAxis) > deadMovementAxis || Mathf.Abs(yAxis) > deadMovementAxis)
        {
            float rot_z = Mathf.Atan2(-yAxis, xAxis) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
        }

        rb2D.AddForce(new Vector2(transform.right.x, transform.right.y).normalized * movementSpeedMultiplier);
        if (rb2D.velocity.sqrMagnitude >= maxSpeed * maxSpeed)
            rb2D.velocity = rb2D.velocity.normalized * maxSpeed;

        currentSpeed = rb2D.velocity.magnitude;
    }

    #region Collisions
    private void OnTriggerEnter2D(Collider2D other)
    {
        House house = other.GetComponent<House>();

        if (house != null && house.owner == this)
        {
            for (int i = 0; i < inv.inventory.Length; i++)
            {
                if (inv.inventory[i] != null && house.AddItem(inv.inventory[i]))
                    inv.Drop(i);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Dump")
        {
            bool buttonA = Input.GetButtonDown("AButton-P" + playerNumber.ToString());
            bool buttonB = Input.GetButtonDown("BButton-P" + playerNumber.ToString());
            bool buttonX = Input.GetButtonDown("XButton-P" + playerNumber.ToString());
            bool buttonY = Input.GetButtonDown("YButton-P" + playerNumber.ToString());

            if (buttonA)
                inv.Drop(0);
            if (buttonB)
                inv.Drop(1);
            if (buttonX)
                inv.Drop(2);
            if (buttonY)
                inv.DropAll();
        }
    }

    #endregion  

}