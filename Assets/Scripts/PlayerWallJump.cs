using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
public class PlayerWallJump : MonoBehaviour
{
    [Header("Wall Settings")]
    public Transform wallCheck;
    public float wallCheckDistance = 0.4f;
    public LayerMask wallLayer;
    public float wallJumpForce = 7f;
    public Vector2 wallJumpDirection = new Vector2(1f, 1f);
    public float wallHoldGravityScale = 0.1f;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    PlayerMovement movementScript;

    bool isTouchingWall;
    bool isWallHolding;
    bool isFacingRight = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        movementScript = GetComponent<PlayerMovement>();

        if (movementScript == null)
            Debug.LogWarning("PlayerWallJump: no se encontró PlayerMovement en el mismo GameObject.");

        wallJumpDirection.Normalize();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        // Preferir flipX del sprite para conocer la orientación real
        isFacingRight = !spriteRenderer.flipX;

        // Detectar pared con Raycast y chequear si golpeó collider del layer
        Vector2 origin = wallCheck != null ? (Vector2)wallCheck.position : (Vector2)transform.position;
        Vector2 dir = isFacingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(origin, dir, wallCheckDistance, wallLayer);
        isTouchingWall = hit.collider != null;

        // Si toca pared y no está en el suelo
        bool grounded = movementScript != null ? movementScript.IsGrounded() : false;

        if (isTouchingWall && !grounded)
        {
            if (isTouchingWall && !grounded)
            {
                EnterWallHold();
            }
            else
            {
                ExitWallHold();
            }

            //// Para que se pegue sólo si se está moviendo hacia la pared:
            //// si el jugador empuja hacia la pared (moveX tiene la misma dirección que dir)
            //if ((isFacingRight && moveX > 0.01f) || (!isFacingRight && moveX < -0.01f))
            //{
            //    EnterWallHold();
            //}
            //else
            //{
            //    // si no empuja hacia la pared, salir del hold
            //    ExitWallHold();
            //}
        }
        else
        {
            ExitWallHold();
        }

        // Salto desde la pared
        if (isWallHolding && Input.GetButtonDown("Jump"))
        {
            WallJump();
        }
        if (isTouchingWall)
            Debug.Log("✅ Tocando pared");
        else
            Debug.Log("❌ No tocando pared");

        Debug.Log($"TouchingWall: {isTouchingWall}, WallHold: {isWallHolding}, VelocityY: {rb.velocity.y}");

    }


    void EnterWallHold()
    {
        if (isWallHolding) return;

        isWallHolding = true;

        // detener la velocidad y reducir la gravedad
        rb.velocity = new Vector2(0f, 0f);
        rb.gravityScale = wallHoldGravityScale;

        animator.SetBool("isWallHolding", true);
        Debug.Log("🧱 Entrando en Wall Hold");

    }

    void ExitWallHold()
    {
        if (!isWallHolding) return;

        isWallHolding = false;
        rb.gravityScale = 1f; // ajustar si tu personaje usa otro gravityScale por defecto
        animator.SetBool("isWallHolding", false);
        Debug.Log("🚶‍♂️ Saliendo del Wall Hold");

    }

    void WallJump()
    {
        ExitWallHold();

        // dirección hacia el lado contrario
        float horizontalDir = isFacingRight ? -1f : 1f;
        Vector2 jumpDir = new Vector2(wallJumpDirection.x * horizontalDir, wallJumpDirection.y).normalized;

        rb.velocity = jumpDir * wallJumpForce;
        animator.SetTrigger("Jump");

        // opcional: voltear sprite para "mirar" hacia la nueva dirección
        spriteRenderer.flipX = (horizontalDir < 0) ? false : true;
    }

    void OnDrawGizmosSelected()
    {
        if (wallCheck != null)
        {
            Gizmos.color = Color.cyan;
            Vector3 dir = (isFacingRight ? Vector3.right : Vector3.left) * wallCheckDistance;
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + dir);
        }
    }
}
