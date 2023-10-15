using Photon.Pun;
using System.Collections;
using UnityEngine;

public class HunterPlayerController : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _distanceCheckToGround;
    [SerializeField] private float _maxDistanceOfRay;
    [SerializeField] private LayerMask _ground;

    private Vector2 _cameraRayDirection;

    private Rigidbody _rigidbody;
    private bool _isShotReload;

    PhotonView PV;
    Camera _camera;
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
        _camera = Camera.main;
        _cameraRayDirection = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    private void Update()
    {
        if (!PV.IsMine) return;
        PlayerMovement();
        PlayerJump();
        PlayerShotMethod();
        StartCoroutine(ShotReload());
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

    private void PlayerShotMethod()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_isShotReload == true) return;

            Ray ray = _camera.ScreenPointToRay(_cameraRayDirection);

            if (Physics.Raycast(ray, out RaycastHit hit, _maxDistanceOfRay))
            {
                if (hit.transform.GetComponent<TestChangeMesh>())
                {
                    _isShotReload = true;
                    Destroy(gameObject);
                }
            }
        }
    }
    private IEnumerator ShotReload()
    {
        _isShotReload = true;
        yield return new WaitForSeconds(1f);
        _isShotReload = false;
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

