using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private GameObject _player;
    private Camera _camera;
    private bool _isInVerticalTrigger = false;
    private bool _isInHorizontalTrigger = false;

    private void Update()
    {
        MoveCamera();
    }

    private void Start()
    {
        Init();
    }

    void MoveCamera()
    {
        if (Manager.Player.Stats.CurHp > 0)
        {
            Vector3 playerPos = _player.transform.position;
            Vector3 cameraPos = _camera.transform.position;

            if (!_isInVerticalTrigger)
                cameraPos.z = playerPos.z;
            if (!_isInHorizontalTrigger)
                cameraPos.x = playerPos.x;

            _camera.transform.position = cameraPos;
        }
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

    void Init()
    {
        _camera = Camera.main;
        _player = Manager.Player.Player;
        _camera.transform.position = new Vector3(0, 60, 0);
        _camera.transform.rotation = Quaternion.Euler(90, 0, 0);
        _camera.fieldOfView = 26f;
        _camera.farClipPlane = 70f;
    }

}
