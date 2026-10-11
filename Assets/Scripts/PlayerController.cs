
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 8f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius = 0.15f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private PlayerInput playerInput;

    private InputAction moveAction;
    private InputAction jumpAction;

    private float moveInput;
    private bool downHeld;
    private bool jumpRequested;
    private bool isDropping;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions.FindAction("Move", true);
        jumpAction = playerInput.actions.FindAction("Jump", true);
    }

    private void Update()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();

        moveInput = input.x;
        downHeld = input.y < -0.5f;

        if (jumpAction.WasPressedThisFrame())
        {
            jumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        Vector2 velocity = rb.linearVelocity;
        velocity.x = moveInput * moveSpeed;

        Collider2D ground = Physics2D.OverlapCircle(
            groundCheck.position,
            checkRadius,
            groundLayer
        );

        if (jumpRequested && !isDropping && ground != null)
        {
            if (downHeld)
            {
                // Chỉ rơi xuyên nền một chiều
                if (ground.GetComponent<PlatformEffector2D>() != null)
                {
                    StartCoroutine(DropThrough(ground));
                    velocity.y = -3f;
                }
            }
            else
            {
                // Nhảy bình thường
                velocity.y = jumpForce;
            }
        }

        jumpRequested = false;
        rb.linearVelocity = velocity;
    }

    private IEnumerator DropThrough(Collider2D platform)
    {
        isDropping = true;

        // Tạm bỏ qua va chạm với nền
        Physics2D.IgnoreCollision(
            playerCollider,
            platform,
            true
        );

        // Chờ cho đến khi Player đi qua nền hoàn toàn
        yield return new WaitForFixedUpdate();

        while (platform != null &&
               playerCollider.bounds.max.y >= platform.bounds.min.y)
        {
            yield return new WaitForFixedUpdate();
        }

        // Bật lại va chạm
        if (platform != null)
        {
            Physics2D.IgnoreCollision(
                playerCollider,
                platform,
                false
            );
        }

        isDropping = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(
            groundCheck.position,
            checkRadius
        );
    }
}
