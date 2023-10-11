using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HunterCameraController : MonoBehaviour
{
    [SerializeField] private float _sesitivity;
    [SerializeField] private Transform _plyer;
    private float _verticalLookRotation;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; 
    }

    
    private void FixedUpdate()
    {
        float MouseX = Input.GetAxis("Mouse X") * _sesitivity * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * _sesitivity * Time.deltaTime;

        _verticalLookRotation -= MouseY;
        _verticalLookRotation = Mathf.Clamp(_verticalLookRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(_verticalLookRotation, 0, 0);
        _plyer.Rotate(MouseX * Vector3.up);
    }
}
