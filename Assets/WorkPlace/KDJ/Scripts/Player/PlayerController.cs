using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float _moveSpeed;

    private Camera _mainCamera;

    private Vector3 _moveDirection;

    private void Awake()
    {
        Init();
    }

    void FixedUpdate()
    {
        PlayerAim();
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        // 플레이어 이동 입력, WASD 이동 / 조이스틱 미대응. 추후 Horizontal, Vertical 변경 할 수도 있음

        Vector3 axis = new Vector3(0, 0, 0);
        
        if (Input.GetKey(KeyCode.W))
        {
            axis.z += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            axis.z -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            axis.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            axis.x += 1;
        }

        axis.Normalize();

        _moveDirection = axis;

        _player.GetComponent<Rigidbody>().MovePosition(_player.transform.position + _moveDirection * _moveSpeed * Time.fixedDeltaTime);
    }

    private void PlayerAim()
    {
        // 플레이어 마우스 조준
        Vector3 lookPos = Input.mousePosition;
        lookPos.z = _mainCamera.transform.position.y - _player.transform.position.y;
        lookPos = _mainCamera.ScreenToWorldPoint(lookPos);
        _player.transform.forward = lookPos - transform.position;
    }

    private void PlayerAttack()
    {
        
    }

    void Init()
    {
        _mainCamera = Camera.main;
    }
}
