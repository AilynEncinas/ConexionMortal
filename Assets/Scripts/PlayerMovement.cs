using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 3f;
    public float jumpForce = 7f;

    [Header("Detección de suelo y pared")]
    public Transform groundCheck;
    public Transform wallCheck;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;
    public LayerMask wallLayer;

    [Header("Wall Jump")]
    public float wallSlideSpeed = 2f;
    public float wallJumpForce = 7f;
    public float wallJumpTime = 0.2f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool isGrounded;
    private bool isTouchingWall;
    private bool isWallSliding;
    private bool isWallJumping;
    private bool canJump = true;

    private float moveX;
    private float wallJumpDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");

        // Detección de suelo y pared
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, checkRadius, wallLayer);

        if (isGrounded)
        {
            canJump = true; // Recupera salto cuando toca el suelo
            isWallJumping = false;
        }

        // Movimiento lateral normal (solo si no está en wall jump)
        if (!isWallJumping)
        {
            rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
        }

        // Animaciones y orientación
        bool walking = Mathf.Abs(moveX) > 0.01f;
        animator.SetBool("isWalking", walking);
        if (moveX > 0.01f) spriteRenderer.flipX = false;
        else if (moveX < -0.01f) spriteRenderer.flipX = true;

        // Wall Slide
        if (isTouchingWall && !isGrounded && rb.velocity.y < 0 && moveX != 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }

        if (isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
            animator.SetBool("isWallSliding", true);
        }
        else
        {
            animator.SetBool("isWallSliding", false);
        }

        // Salto normal (solo 1 vez hasta volver al suelo)
        if (Input.GetButtonDown("Jump") && canJump && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("Jump");
            canJump = false;
        }

        // Wall Jump
        if (Input.GetButtonDown("Jump") && isTouchingWall && !isGrounded)
        {
            isWallJumping = true;
            wallJumpDirection = spriteRenderer.flipX ? 1 : -1; // Salta en dirección contraria a la pared
            rb.velocity = new Vector2(wallJumpDirection * wallJumpForce, jumpForce);
            animator.SetTrigger("Jump");
            Invoke(nameof(StopWallJump), wallJumpTime);
        }
    }

    void StopWallJump()
    {
        isWallJumping = false;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        }

        if (wallCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(wallCheck.position, checkRadius);
        }
    }
}
