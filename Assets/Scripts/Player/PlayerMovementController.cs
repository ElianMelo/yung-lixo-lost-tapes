using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float rotationSpeed;
    public float rotationFactor;

    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;

    public float dashSpeed;
    public float dashSpeedChangeFactor;

    public float maxYSpeed;

    public float groundDrag;

    [Header("Dashing")]
    public float dashForce;

    [Header("Jumping")]
    public float jumpForce;
    public float airMultiplier;
    public float knockback;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    private bool grounded;
    private bool running;
    private bool backwards;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;

    public Transform orientation;
    public Transform cameraOrientation;
    public Transform slopeDetectorFront;
    public Transform slopeDetectorBack;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody playerRb;

    private int maxJumps = 2;
    private int jumps;

    public MovementState state;

    private Animator playerAnimator;
    public enum MovementState
    {
        walking,
        sprinting,
        dashing,
        air
    }

    public bool dashing;

    private readonly static string IsBackwards = "IsBackwards";
    private readonly static string IsGrounded = "IsGrounded";
    private readonly static string IsRunning = "IsRunning";
    private readonly static string JumpAnim = "Jump";
    private readonly static string JumpTwoAnim = "JumpTwo";
    private readonly static string KickAnim = "Kick";

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        playerRb.freezeRotation = true;
        jumps = maxJumps;
    }

    private void Update()
    {
        if (PauseMananger.Instance.CurrentState == GamePauseState.Paused) return;
        // grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        if (grounded) jumps = maxJumps;

        GetInputs();
        SpeedControl();
        StateHandler();
        CheckAnimation();

        playerRb.velocity = new Vector3(
            Mathf.Clamp(playerRb.velocity.x, playerRb.velocity.x, 20f),
            Mathf.Clamp(playerRb.velocity.y, playerRb.velocity.y, 20f),
            Mathf.Clamp(playerRb.velocity.z, playerRb.velocity.z, 20f));

        if (state == MovementState.walking ||
            state == MovementState.sprinting)
        {
            playerRb.drag = groundDrag;
        }
        else
        {
            playerRb.drag = 0f;
        }
    }

    private void FixedUpdate()
    {
        if (PauseMananger.Instance.CurrentState == GamePauseState.Paused) return;
        Move();
        RotatePlayer();
    }

    private void GetInputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && jumps > 0)
        {
            jumps -= 1;
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            Kick();
        }
    }

    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;
    private MovementState lastState;
    private bool keepMomentum;

    private void StateHandler()
    {
        // Mode - Dashing
        if (dashing)
        {
            state = MovementState.dashing;
            desiredMoveSpeed = dashSpeed;
            speedChangeFactor = dashSpeedChangeFactor;
        }
        // Mode - Sprinting
        else if (grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            desiredMoveSpeed = sprintSpeed;
        }
        // Mode - Walking
        else if (grounded)
        {
            state = MovementState.walking;
            desiredMoveSpeed = walkSpeed;
        }
        // Mode - Air
        else
        {
            state = MovementState.air;
            if (desiredMoveSpeed < sprintSpeed)
                desiredMoveSpeed = walkSpeed;
            else
                desiredMoveSpeed = sprintSpeed;
        }

        bool desiredMoveSpeedHasChanged = desiredMoveSpeed != lastDesiredMoveSpeed;
        if (lastState == MovementState.dashing) keepMomentum = true;

        if (desiredMoveSpeedHasChanged)
        {
            if (keepMomentum)
            {
                StopAllCoroutines();
                StartCoroutine(SmoothlyLerpMoveSpeed());
            }
            else
            {
                StopAllCoroutines();
                moveSpeed = desiredMoveSpeed;
            }
        }

        lastDesiredMoveSpeed = desiredMoveSpeed;
        lastState = state;
    }

    private float speedChangeFactor;

    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        // smoothly lerp movementSpeed to desired value
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        float boostFactor = speedChangeFactor;

        while (time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);
            time += Time.deltaTime * boostFactor;
            yield return null;
        }

        moveSpeed = desiredMoveSpeed;
        speedChangeFactor = 1f;
        keepMomentum = false;
    }

    private void Move()
    {
        if (state == MovementState.dashing) return;

        moveDirection = orientation.forward * verticalInput;

        float currentMoveSpeed = 0f;

        if(backwards)
        {
            currentMoveSpeed = moveSpeed / 2;
        } else
        {
            currentMoveSpeed = moveSpeed;
        }

        // on slope
        if (OnSlope())
        {
            playerRb.AddForce(GetSlopMoveDirection() * currentMoveSpeed * 20f, ForceMode.Force);

            // Check!!!
            //if (playerRb.velocity.y > 0)
            //    playerRb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        // on ground
        if (grounded)
            playerRb.AddForce(moveDirection.normalized * currentMoveSpeed * 10f, ForceMode.Force);
        // in air
        else if (!grounded)
            playerRb.AddForce(moveDirection.normalized * currentMoveSpeed * 10f * airMultiplier, ForceMode.Force);
        // turn gravity off while on slope
        // playerRb.useGravity = !OnSlope();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

    private void SpeedControl()
    {
        // limiting speed on slop
        if (OnSlope())
        {
            if (playerRb.velocity.magnitude > moveSpeed)
            {
                playerRb.velocity = playerRb.velocity.normalized * moveSpeed;
            }
        }
        // limiting speed on ground or in air
        else
        {
            Vector3 flatVel = new Vector3(playerRb.velocity.x, 0f, playerRb.velocity.z);

            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                playerRb.velocity = new Vector3(limitedVel.x, playerRb.velocity.y, limitedVel.z);
            }
        }

        // limit y vel
        if (maxYSpeed != 0 && playerRb.velocity.y > maxYSpeed)
        {
            playerRb.velocity = new Vector3(playerRb.velocity.x, maxYSpeed, playerRb.velocity.z);
        }
    }
    private void Kick()
    {
        MusicSystem.Instance.PlaySound(SoundEffects.Attack);
        playerAnimator.SetTrigger(KickAnim);
    }

    public void Jump(bool doubleForce = false)
    {
        MusicSystem.Instance.PlaySound(SoundEffects.Jump);
        if(grounded)
        {
            playerAnimator.SetTrigger(JumpAnim);
        } else
        {
            playerAnimator.SetTrigger(JumpTwoAnim);
        }
        var calculatedForce = doubleForce ? jumpForce * 2 : jumpForce;
        playerRb.velocity = new Vector3(playerRb.velocity.x, 0f, playerRb.velocity.z);
        playerRb.AddForce(transform.up * calculatedForce, ForceMode.Impulse);
    }

    public void Knockback(Vector3 direction)
    {
        var calculatedForce = knockback * 2;
        playerRb.AddForce((direction) * calculatedForce, ForceMode.Impulse);
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(slopeDetectorFront.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    private Vector3 GetSlopMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

    // Keep old
    private void CheckAnimation()
    {
        running = !(horizontalInput == 0 && verticalInput == 0);
        backwards = verticalInput < 0;

        playerAnimator.SetBool(IsBackwards, backwards);
        playerAnimator.SetBool(IsGrounded, grounded);
        playerAnimator.SetBool(IsRunning, running);
    }

    private void RotatePlayer()
    {
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0f, horizontalInput * rotationSpeed, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationFactor);
    }
}
