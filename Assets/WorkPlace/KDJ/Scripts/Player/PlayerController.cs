using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float _moveSpeed;

    private Vector3 _moveDirection;

    void FixedUpdate()
    {
        PlayerAim();
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        // �÷��̾� �̵� �Է�, WASD �̵� / ���̽�ƽ �̴���. ���� Horizontal, Vertical ���� �� ���� ����

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
        
    }

    private void PlayerAttack()
    {
        
    }
}
