using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public CameraController cam;
    private Rigidbody rb;

    [Header("Movement")]
    public float speed = 8f;
    public float maxSpeed = 30f;
    public float jumpForce = 7f;
    public float groundDrag = 5f;
    public float airMultiplier = 0.6f;

    [Header("Ground")]
    public LayerMask groundLayer;
    bool grounded;

    [Header("Wall Run")]
    public LayerMask wallLayer;
    public float wallRunForce = 20f;
    public float wallGravity = 8f;
    public float wallCheckDistance = 1f;
    public float maxWallRunTime = 1.5f;

    private bool wallRunning;
    private float wallRunTimer;
    private RaycastHit wallHit;

    Vector3 moveDir;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        InputHandler();
        GroundCheck();
        RotatePlayer();
        HandleWallRun();
    }

    void FixedUpdate()
    {
        Move();
    }

    // ================= INPUT =================
    void InputHandler()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        moveDir = orientation.forward * z + orientation.right * x;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded)
                Jump();
            else if (wallRunning)
                WallJump();
        }
    }

    // ================= MOVEMENT =================
    void Move()
    {
        if (wallRunning) return;

        rb.linearDamping = grounded ? groundDrag : 0;

        if (grounded)
            rb.AddForce(moveDir.normalized * speed * 10f, ForceMode.Force);
        else
            rb.AddForce(moveDir.normalized * speed * 10f * airMultiplier, ForceMode.Force);

        LimitSpeed();
    }

    void LimitSpeed()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        if (flatVel.magnitude > maxSpeed)
        {
            Vector3 limited = flatVel.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(limited.x, rb.linearVelocity.y, limited.z);
        }
    }

    void Jump()
    {
        rb.useGravity = true;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    // ================= WALL RUN SYSTEM =================
    void HandleWallRun()
    {
        bool wallRight = Physics.Raycast(transform.position, orientation.right, out wallHit, wallCheckDistance, wallLayer);
        bool wallLeft = Physics.Raycast(transform.position, -orientation.right, out wallHit, wallCheckDistance, wallLayer);

        if ((wallLeft || wallRight) && !grounded && rb.linearVelocity.y <= 2f)
        {
            if (!wallRunning)
                StartWallRun();

            WallRunMovement();
        }
        else if (wallRunning)
        {
            StopWallRun();
        }
    }

    void StartWallRun()
    {
        wallRunning = true;
        wallRunTimer = maxWallRunTime;
        rb.useGravity = false;
    }

    void WallRunMovement()
    {
        wallRunTimer -= Time.deltaTime;

        // Apply downward force so you don't float
        rb.AddForce(Vector3.down * wallGravity, ForceMode.Force);

        // Move along wall
        Vector3 wallForward = Vector3.Cross(wallHit.normal, Vector3.up);

        if (Vector3.Dot(wallForward, orientation.forward) < 0)
            wallForward = -wallForward;

        rb.AddForce(wallForward * wallRunForce, ForceMode.Force);

        if (wallRunTimer <= 0)
            StopWallRun();
    }

    void StopWallRun()
    {
        wallRunning = false;
        rb.useGravity = true;
    }

    void WallJump()
    {
        StopWallRun();

        Vector3 jumpDir = wallHit.normal + Vector3.up;

        rb.linearVelocity = Vector3.zero;
        rb.AddForce(jumpDir * jumpForce * 1.5f, ForceMode.Impulse);
    }

    // ================= ROTATION =================
    void RotatePlayer()
    {
        float y = cam.GetYRotation();

        orientation.rotation = Quaternion.Slerp(
            orientation.rotation,
            Quaternion.Euler(0, y, 0),
            Time.deltaTime * 12f
        );
    }

    // ================= GROUND =================
    void GroundCheck()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);

        if (grounded && wallRunning)
            StopWallRun();
    }
}
