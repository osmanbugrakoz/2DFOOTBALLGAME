using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public bool isPlayer1 = true;

    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = 0f;
        
        if (isPlayer1)
        {
            if (Input.GetKey(KeyCode.A)) moveInput = -1f;
            if (Input.GetKey(KeyCode.D)) moveInput = 1f;
            
            if (Input.GetKeyDown(KeyCode.W) && isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
                isGrounded = false;
            }
        }
       
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow)) moveInput = -1f;
            if (Input.GetKey(KeyCode.RightArrow)) moveInput = 1f;

            if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
                isGrounded = false;
            }
        }
  
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocityY);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                isGrounded = true;
                break;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded=false;    
    }

}
