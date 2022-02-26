using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _health = 0;
    [SerializeField] private float _playerSpeed = 0;
    [SerializeField] private float _playerSprintSpeed = 0;
    [SerializeField] private float _playerCrouchSpeed = 0;
    [SerializeField] private float _jumpForce = 0;
    [SerializeField] private bool _canJump = true;
    [SerializeField] private bool _isGrounded = true;
    [SerializeField] private bool _isCrouching = false;
    [SerializeField] private bool _isRunnning= false;
    [SerializeField] private LayerMask _ground;

    private Rigidbody _rb;
    private Animator _animator;
    private Vector3 _crouchScale;
    private Vector3 _idleScale;
    private float _constPlayerSpeed;
    // on future;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _crouchScale = new Vector3 (transform.localScale.x, transform.localScale.y / 2, transform.localScale.z);
        _idleScale = transform.localScale;
        _constPlayerSpeed = _playerSpeed;
    }
    private void Update()
    {
        Jump();
        CrouchState();
        SprintState();
    }
    private void FixedUpdate() => Movement();
    private void Movement()
    { 
        Vector2 _playerDelta = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * _playerSpeed * Time.deltaTime;
        Vector3 _movement = transform.right * _playerDelta.x + transform.forward * _playerDelta.y;
        _rb.velocity = new Vector3(_movement.x, _rb.velocity.y, _movement.z);
    }
    private void Jump()
    {
        _isGrounded = Physics.CheckSphere(new Vector3(transform.localPosition.x, transform.localPosition.y - 1, transform.localPosition.z), 0.6f, _ground);
        if ( _canJump && Input.GetButtonDown("Jump") && _isGrounded && !_isCrouching)
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            StartCoroutine(JumpCoolDown(2.0f));
        }
    }
    private void CrouchState()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !_isRunnning && _canJump)
        {
            _isCrouching = true;
            transform.localScale = _crouchScale;  
            _playerSpeed = _playerCrouchSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            _isCrouching = false;
            transform.localScale = _idleScale;
            _playerSpeed = _constPlayerSpeed;
        }
    }
    private void SprintState()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !_isCrouching && _canJump)
        {
            _isRunnning = true;
            _playerSpeed = _playerSprintSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _isRunnning = false;
            _playerSpeed = _constPlayerSpeed ;
        }
    }
    private IEnumerator JumpCoolDown(float seconds)
    {
        _canJump = false;
        yield return new WaitForSeconds(seconds);
        _canJump = true;
    }
}
