using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    HunterPlayerController hunterPlayerController;

    private void Awake() => hunterPlayerController = GetComponentInParent<HunterPlayerController>();
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == hunterPlayerController.gameObject) return;
        hunterPlayerController.SetGroundedState(true);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == hunterPlayerController.gameObject) return;
        hunterPlayerController.SetGroundedState(false);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == hunterPlayerController.gameObject) return;
        hunterPlayerController.SetGroundedState(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == hunterPlayerController.gameObject) return;
        hunterPlayerController.SetGroundedState(true);
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == hunterPlayerController.gameObject) return;
        hunterPlayerController.SetGroundedState(false);
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject == hunterPlayerController.gameObject) return;
        hunterPlayerController.SetGroundedState(false);
    }
}
