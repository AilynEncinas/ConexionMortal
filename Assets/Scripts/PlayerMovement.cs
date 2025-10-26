using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 3f;
    public float jumpForce = 7f;
    public Transform groundCheck;        
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;       

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);

        bool walking = Mathf.Abs(moveX) > 0.01f;
        animator.SetBool("isWalking", walking);

        if (moveX > 0.01f) spriteRenderer.flipX = false;
        else if (moveX < -0.01f) spriteRenderer.flipX = true;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("Jump");
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
