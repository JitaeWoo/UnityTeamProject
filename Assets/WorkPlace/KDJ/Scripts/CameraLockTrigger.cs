using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLockTrigger : MonoBehaviour
{
    private CameraMove _camera;

    private void Awake()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMove>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && this.gameObject.name == "VerticalT")
        {
            _camera.CameraAxisLock(gameObject);
        }
        if (other.CompareTag("Player") && this.gameObject.name == "HorizonT")
        {
            _camera.CameraAxisLock(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && this.gameObject.name == "VerticalT")
        {
            _camera.CameraAxisUnlock(gameObject);
        }
        if (other.CompareTag("Player") && this.gameObject.name == "HorizonT")
        {
            _camera.CameraAxisUnlock(gameObject);
        }
    }

}
