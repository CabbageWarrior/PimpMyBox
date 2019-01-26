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

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

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
}