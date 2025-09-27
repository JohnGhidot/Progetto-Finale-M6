using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AnimationController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rb;
    private PlayerController _playerController;
    private bool _wasGrounded = true;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody>();
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        Vector3 planarVel = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
        float speed = planarVel.magnitude;
        _animator.SetFloat("Speed", speed);

        bool isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        _animator.SetBool("IsGrounded", isGrounded);
        _animator.SetFloat("VerticalVelocity", _rb.velocity.y);

        if (_wasGrounded && !isGrounded && _rb.velocity.y > 0.1f)
        {
            _animator.SetTrigger("Jump");
        }

        _wasGrounded = isGrounded;

        //if (_animator != null)
        //{
        //    Debug.Log("Animator trovato: " + _animator);
        //}

    }
}