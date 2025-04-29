using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

public class Movement : Main
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected GroundCheck groundCheck;
    [SerializeField] protected Animator animator;
    [SerializeField] protected PlayerCtrl playerControl;
    [SerializeField] protected SpriteRenderer spriteRenderer;

    protected PlayerState currentState;
    protected PlayerState previousState;

    // Variables for speed and jump force adjustment

    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected float jumpForce = 5f;
    [SerializeField] protected float dashDuration = 0.25f;
    [SerializeField] protected float dashTimeLeft = 0f;
    [SerializeField] protected float dashSpeed = 20;
    [SerializeField] protected float spriteOffset;
    [SerializeField] protected bool isDashing = false;

    // Status checking variables
    protected bool isGrounded;
    protected bool canDoubleJump;


    protected override void Start()
    {
        base.Start();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    protected virtual void Update()
    {
        Run();
        Jump();
        CheckingState();
    }
    protected virtual void LateUpdate()
    {
        Dashing();
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        LoadRigidbody();
        LoadAnimator();
        LoadGround();
        LoadPlayerCtrl();
        LoadSpriteRenderer();
    }
    protected virtual void LoadSpriteRenderer()
    {
        if (spriteRenderer != null) return;
        spriteRenderer = transform.parent.GetComponentInChildren<SpriteRenderer>();
        spriteOffset = spriteRenderer.sprite.bounds.size.x;
    }
    protected virtual void LoadPlayerCtrl()
    {
        if (playerControl != null) return;
        playerControl = transform.parent.GetComponent<PlayerCtrl>();
    }
    protected virtual void LoadRigidbody()
    {
        if (rb != null) return;
        rb = transform.parent.GetComponent<Rigidbody2D>();
    }
    protected virtual void LoadAnimator()
    {
        if (animator) return;
        animator = transform.parent.GetComponentInChildren<Animator>();
    }
    protected virtual void LoadGround()
    {
        if (groundCheck != null) return;
        groundCheck = transform.parent.GetComponentInChildren<GroundCheck>();
    }

    protected virtual void Dashing()
    {
        if (InputManager.Instance != null && CombatHotKey.Instance.IsDashing())
        {
            isDashing = true;
            dashTimeLeft = dashDuration;
            Debug.Log("LocalScale.x: " + playerControl.transform.localScale.x);
            float dashDirection = playerControl.transform.localScale.x > 0 ? 1f : -1f;
            rb.linearVelocity = new Vector2(dashDirection * dashSpeed, rb.linearVelocity.y);
            animator.SetTrigger("Dashing");
        }

        if (isDashing)
        {
            dashTimeLeft -= Time.deltaTime;
            if (dashTimeLeft <= 0)
            {
                isDashing = false;
            }
        }
    }
    protected virtual void Run()
    {
        if (isDashing) return;

        Vector3 scale = playerControl.transform.localScale;
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y); // Change linearVelocity to velocity

        if (moveInput > 0)
        {
            if (scale.x < 0)
            {
                scale.x = 1;
                playerControl.transform.localScale = scale;
                // Điều chỉnh lại vị trí để ngăn nhân vật bị dịch khi flip
                Vector3 position = playerControl.transform.position;
                position.x += 0.5f; // Điều chỉnh giá trị này tùy theo kích thước nhân vật
                playerControl.transform.position = position;
            }
        }
        else if (moveInput < 0)
        {
            if (scale.x > 0)
            {
                scale.x = -1;
                playerControl.transform.localScale = scale;
                // Điều chỉnh lại vị trí để ngăn nhân vật bị dịch khi flip
                Vector3 position = playerControl.transform.position;
                position.x -= 0.5f; // Điều chỉnh giá trị này tùy theo kích thước nhân vật
                playerControl.transform.position = position;
            }
        }
    }
    protected virtual void Jump()
    {
        if (isDashing) return;
        isGrounded = groundCheck.IsGrounded;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                canDoubleJump = true;
            }
            else if (canDoubleJump)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                canDoubleJump = false;
            }
        }
    }
    protected virtual void CheckingState()
    {
        float horizontalVelocity = Mathf.Abs(rb.linearVelocity.x);
        float verticalVelocity = rb.linearVelocity.y;
        previousState = currentState;
        animator.SetBool("Idling", false);
        animator.SetBool("Running", false);
        animator.SetBool("Jumping", false);
        animator.SetBool("Falling", false);
        animator.SetBool("Dashing", false);
        if (isGrounded)
        {
            // Khi chạm đất
            if (horizontalVelocity < 0.1f)
            {
                currentState = PlayerState.Idling;
                animator.SetBool("Idling", true);
            }
            else
            {
                currentState = PlayerState.Running;
                animator.SetBool("Running", true);
            }
        }
        else
        {
            if (verticalVelocity > 0)
            {
                currentState = PlayerState.Jumping;
                animator.SetBool("Jumping", true);
            }
            else if (verticalVelocity < 0)
            {
                currentState = PlayerState.Falling;
                animator.SetBool("Falling", true);
            }
        }
        if (currentState != previousState) Debug.Log("Current State: " + currentState + " Vertical: "+ verticalVelocity);

    }
    public virtual PlayerState CurrentState()
    {
        return currentState;
    }

    public virtual float verticalVelocity()
    {
        return rb.linearVelocity.y;  
    }


}