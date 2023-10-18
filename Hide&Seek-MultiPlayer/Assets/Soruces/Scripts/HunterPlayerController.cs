using Photon.Pun;
using System.Collections;
using UnityEngine;

public class HunterPlayerController : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _distanceCheckToGround;
    [SerializeField] private LayerMask _ground;

    private Rigidbody _rigidbody;

    PhotonView PV;

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
            Destroy(_rigidbody);
        }

    }

    private void Update()
    {
        if (!PV.IsMine) return;
        PlayerMovement();
        PlayerJump();
    }

    private void FixedUpdate()
    {
        if (!PV.IsMine) return;
    }

    private void PlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 temp = (((transform.right * horizontal) + (transform.forward * vertical)) * _walkSpeed);
        _rigidbody.velocity = new Vector3(temp.x, _rigidbody.velocity.y, temp.z);
    }

   
    private void PlayerJump()
    {
        if (Physics.Raycast(transform.position, Vector3.down, _distanceCheckToGround, _ground))
        {
            if (Input.GetKeyDown(KeyCode.Space)) _rigidbody.velocity = Vector3.up *  _jumpForce;   
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * _distanceCheckToGround);
    }
}

