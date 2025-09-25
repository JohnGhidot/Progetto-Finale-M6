using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _Speed = 5f;
    [SerializeField] private float _JumpForce = 5f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius = 0.2f;    

    [Header("Setting Tank Controls")]
    [SerializeField] private float _RotationSpeed = 220f;
    [SerializeField] private float _DeadZone = 0.1f;
    [SerializeField] private bool _InvertTurnWhenReversing = true;

    private Rigidbody _rb;
    private bool isGrounded;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckGroundStatus();
        HandleJump();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        

        if (Mathf.Abs(h) < _DeadZone)
        {
            h = 0f;
        }
        if (Mathf.Abs(v) < _DeadZone)
        {
            v = 0f;
        }

        if (_InvertTurnWhenReversing == true)
        {
            if (v < 0f)
            {
                h = -h;
            }
        }

        Vector3 currentVel = _rb.velocity;
        Vector3 planarVel = transform.forward * (v * _Speed);
        _rb.velocity = new Vector3(planarVel.x, currentVel.y, planarVel.z);

        if (h != 0f)
        {
            float deltaDegrees = h * _RotationSpeed * Time.fixedDeltaTime;
            Quaternion deltaRot = Quaternion.Euler(0f, deltaDegrees, 0f);
            _rb.MoveRotation(_rb.rotation * deltaRot);
        }
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            _rb.AddForce(Vector3.up * _JumpForce, ForceMode.Impulse);
        }
    }

    private void CheckGroundStatus()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
