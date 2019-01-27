using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public System.Action<GameObject> onBeerTaken;

    public SpriteRenderer[] spriteRenderers;
    public Image dashImage;
    public Image dashAvailableImage;
    public Image vomitAvailableImage;
    [Space]
    public int playerNumber = 1;

    public float movementSpeedMultiplier = 1f;
    public float deadMovementAxis = .19f;
    public float maxSpeed = 10f;

    [Space]
    public float dashDeadAmount = .7f;

    public float dashFillTime = 2.5f;
    public float dashCooldownTime = 5f;
    public float dashSpeedMaxImpulse = 25;

    private float currentFillTime = 0f;
    private float currentCooldownTime = 0f;

    private bool canChargeDash = true;

    private bool isDashing = false;

    private Rigidbody2D rb2D;

    private GameInterfaceController gic;

    private PlayerInventory inv;
    public PlayerInventory Inv { get { return inv; } }

    bool canVomit = false;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        inv = GetComponent<PlayerInventory>();

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].gameObject.SetActive(i == playerNumber - 1);
        }

        gic = FindObjectOfType<GameInterfaceController>();

        inv.RemovedFromInventory += (a) => { gic.RefreshUI(); };
    }

    private void Update()
    {
        float xAxis = Input.GetAxis("Horizontal-P" + playerNumber.ToString());
        float yAxis = Input.GetAxis("Vertical-P" + playerNumber.ToString());

        float rTriggerAxis = Input.GetAxis("RightTrigger-P" + playerNumber.ToString());


        float impulseAmount = 0f;

        if (rTriggerAxis > dashDeadAmount)
        {
            // Thumb in Switch position.
            if (canChargeDash)
            {
                currentFillTime += Time.deltaTime;
                if (currentFillTime >= dashFillTime)
                {
                    currentFillTime = dashFillTime;
                    canChargeDash = false;
                    dashAvailableImage.gameObject.SetActive(canChargeDash);
                    isDashing = true;

                    impulseAmount = dashSpeedMaxImpulse;
                    currentFillTime = 0;
                    dashImage.fillAmount = 0;
                }
                else
                {
                    dashImage.fillAmount = currentFillTime / dashFillTime;
                }
            }
        }
        else
        {
            // Thumb in Relax position.
            if (canChargeDash && currentFillTime > 0f)
            {
                canChargeDash = false;
                dashAvailableImage.gameObject.SetActive(canChargeDash);
                isDashing = true;
                AudioSingleton.PlaySound(AudioSingleton.Sound.Dash);

                impulseAmount = dashSpeedMaxImpulse * currentFillTime / dashFillTime;
                currentFillTime = 0;
                dashImage.fillAmount = 0;
            }
        }

        if ((rb2D.velocity.sqrMagnitude <= maxSpeed * maxSpeed) && Mathf.Abs(xAxis) > deadMovementAxis || Mathf.Abs(yAxis) > deadMovementAxis)
        {
            float rot_z = Mathf.Atan2(-yAxis, xAxis) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
        }

        if (!canChargeDash)
        {
            if (isDashing)
            {
                isDashing = false;
                rb2D.angularVelocity = 0;
                rb2D.velocity = rb2D.velocity.normalized * impulseAmount;
            }
            currentCooldownTime += Time.deltaTime;
            if (currentCooldownTime >= dashCooldownTime)
            {
                canChargeDash = true;
                dashAvailableImage.gameObject.SetActive(canChargeDash);
                currentCooldownTime = 0;
            }
        }

        if (rb2D.velocity.sqrMagnitude <= maxSpeed * maxSpeed)
        {
            rb2D.AddForce(new Vector2(transform.right.x, transform.right.y).normalized * movementSpeedMultiplier);
            if (rb2D.velocity.sqrMagnitude >= maxSpeed * maxSpeed)
                rb2D.velocity = rb2D.velocity.normalized * maxSpeed;
        }



    }

    private void Vomit()
    {
        canVomit = false;
        AudioSingleton.PlaySound(AudioSingleton.Sound.Vomit);
        vomitAvailableImage.gameObject.SetActive(canVomit);
    }

    #region Collisions
    private void OnTriggerEnter2D(Collider2D other)
    {
        House house = other.GetComponent<House>();

        if (house != null && house.owner == this)
        {
            bool almostOneToHome = false;

            for (int i = 0; i < inv.inventory.Length; i++)
            {
                if (inv.inventory[i] != null && house.AddItem(inv.inventory[i]))
                {
                    inv.Drop(i, false);
                    almostOneToHome = true;
                }

                if (almostOneToHome)
                    AudioSingleton.PlaySound(AudioSingleton.Sound.DropObject);
            }

            gic.RefreshUI();
        }

        if (other.tag == "Beer")
        {
            if (onBeerTaken != null)
                onBeerTaken.Invoke(other.gameObject);
            canVomit = true;
            vomitAvailableImage.gameObject.SetActive(canVomit);
            AudioSingleton.PlaySound(AudioSingleton.Sound.PickupObject);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Dump")
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

        if (collision.GetComponent<House>())
        {
            float lTriggerAxis = Input.GetAxis("LeftTrigger-P" + playerNumber.ToString());
            if (lTriggerAxis > dashDeadAmount)
            {
                if (canVomit && collision.GetComponent<House>().owner != this)
                {
                    if (collision.GetComponent<House>().RemoveRandomItem())
                        Vomit();
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            AudioSingleton.PlaySound(AudioSingleton.Sound.PVPBounce);
        }
    }

    #endregion  

}