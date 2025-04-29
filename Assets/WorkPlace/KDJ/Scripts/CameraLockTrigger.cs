using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLockTrigger : MonoBehaviour
{
    [SerializeField] private CameraMove Camera;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && this.gameObject.name == "VerticalT")
        {
            Camera.CameraAxisLock(gameObject);
        }
        if (other.CompareTag("Player") && this.gameObject.name == "HorizonT")
        {
            Camera.CameraAxisLock(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && this.gameObject.name == "VerticalT")
        {
            Camera.CameraAxisUnlock(gameObject);
        }
        if (other.CompareTag("Player") && this.gameObject.name == "HorizonT")
        {
            Camera.CameraAxisUnlock(gameObject);
        }
    }

}
