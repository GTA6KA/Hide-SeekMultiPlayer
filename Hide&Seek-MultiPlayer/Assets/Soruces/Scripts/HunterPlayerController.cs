using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HunterPlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _cameraHolder;
    [SerializeField] private float _mouseSensitivity, _runSpeed, _walkSpeed, _jumpForce, _smoothTime;

    private float _verticalLookRotation;

    private Vector3 _smoothMoveVelocity;
    private Vector3 _moveAmount;

    private Rigidbody _rigidBody;
     bool _isGrounded;

    private PhotonView PV;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
    }
    private void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);  
        }
    }

    private void Update()
    {
        if (!PV.IsMine) return;
    
        PlayerCameraController();
        PlayerMovement();
        PlayerJump();
    }
    private void FixedUpdate()
    {
        _rigidBody.MovePosition(_rigidBody.position + transform.TransformDirection(_moveAmount) * Time.fixedDeltaTime);
    }
    private void PlayerMovement()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        _moveAmount = Vector3.SmoothDamp(_moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? _runSpeed : _walkSpeed), ref _smoothMoveVelocity, _smoothTime);     
    }
    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded) _rigidBody.AddForce(transform.up * _jumpForce);
    }
    private void PlayerCameraController()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * _mouseSensitivity);

        _verticalLookRotation += Input.GetAxisRaw("Mouse Y") * _mouseSensitivity;
        _verticalLookRotation = Mathf.Clamp(_verticalLookRotation, -90, 90);

        _cameraHolder.transform.localEulerAngles = Vector3.left * _verticalLookRotation;
    }
    
    public void SetGroundedState(bool _grounded)
    {
        _isGrounded = _grounded;
    }
}
