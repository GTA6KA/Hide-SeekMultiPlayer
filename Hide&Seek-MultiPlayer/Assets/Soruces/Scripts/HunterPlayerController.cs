using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HunterPlayerController : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity, _runSpeed, _walkSpeed, _jumpForce, _smoothTime;

    private Rigidbody _rigidbody;
    private PhotonView PV;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
    
        //PlayerCameraController();
        PlayerMovement();
        //PlayerJump();
    }
    //private void FixedUpdate()
    //{
    //    _rigidBody.MovePosition(_rigidBody.position + transform.TransformDirection(_moveAmount) * Time.fixedDeltaTime);
    //}
    private void PlayerMovement()
    {
         float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 temp = (((transform.right * horizontal) + (transform.forward * vertical)) * _walkSpeed);
        _rigidbody.velocity = new Vector3(temp.x, _rigidbody.velocity.y, temp.z);

        if (_rigidbody.velocity == new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _jumpForce, _rigidbody.velocity.z);
            }
        }
    }
    //private void PlayerJump()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space) && _isGrounded) _rigidBody.AddForce(transform.up * _jumpForce);
    //}
    //private void PlayerCameraController()
    //{
    //    transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * _mouseSensitivity);

    //    _verticalLookRotation += Input.GetAxisRaw("Mouse Y") * _mouseSensitivity;
    //    _verticalLookRotation = Mathf.Clamp(_verticalLookRotation, -90, 90);

    //    _cameraHolder.transform.localEulerAngles = Vector3.left * _verticalLookRotation;
    //}
}
