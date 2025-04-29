using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _camera;

    private bool _isInVerticalTrigger = false;
    private bool _isInHorizontalTrigger = false;

    private void Update()
    {
        MoveCamera();
    }


    void MoveCamera()
    {
        Vector3 playerPos = _player.transform.position;
        Vector3 cameraPos = _camera.transform.position;

        if(!_isInVerticalTrigger)
            cameraPos.z = playerPos.z;
        if (!_isInHorizontalTrigger)
            cameraPos.x = playerPos.x;

        _camera.transform.position = cameraPos;
    }

    public void CameraAxisLock(GameObject gameObject)
    {
        switch (gameObject.name)
        {
            case "VerticalT":
                _isInVerticalTrigger = true;
                break;
            case "HorizonT":
                _isInHorizontalTrigger = true;
                break;
        }
    }

    public void CameraAxisUnlock(GameObject gameObject)
    {
        switch (gameObject.name)
        {
            case "VerticalT":
                _isInVerticalTrigger = false;
                break;
            case "HorizonT":
                _isInHorizontalTrigger = false;
                break;
        }
    }

}
