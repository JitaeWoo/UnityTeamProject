using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamagable
{
    [SerializeField] private GameObject _player;
    [SerializeField] int _poolSize;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _muzzlePosition;

    [Header("Test")]
    [SerializeField] private int _bulletProjectileNum;
    [SerializeField] private float _invincibleTime;

    // 플레이어 관련
    private Camera _mainCamera;
    private Vector3 _moveDirection;
    private bool IsDamagable = true;
    private Coroutine _invincibleRoutine;

    // 탄환 풀
    private Stack<GameObject> _bulletPool;
    private WaitForSeconds _waitTime;
    private Coroutine _fireCoroutine;
    private bool IsReadyFire => _fireCoroutine == null;

    private void Awake()
    {
        Init();
    }

    void FixedUpdate()
    {
        PlayerAim();
        PlayerMovement();
    }

    private void Update()
    {
        PlayerAttack();
    }

    private void PlayerMovement()
    {
        // 플레이어 이동 입력, WASD 이동 / 조이스틱 미대응. 추후 Horizontal, Vertical로 변경 할 수도 있음
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

        _player.GetComponent<Rigidbody>().MovePosition(_player.transform.position + _moveDirection * Manager.Player.Stats.Speed * Time.fixedDeltaTime);
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
        // 플레이어 탄환 발사
        if (Input.GetMouseButton(0))
        {
            if (IsReadyFire)
            {
                _fireCoroutine = StartCoroutine(Fire());
            }
        }
    }

    public void TakeHit(int damage)
    {
        if (IsDamagable)
            Manager.Player.Stats.CurHp -= damage;
        if (_invincibleRoutine == null)
            _invincibleRoutine = StartCoroutine(Invincibility());
    }

    void Init()
    {
        // 기본 초기화 과정
        _mainCamera = Camera.main;

        // 아래는 임시 테스트용
        Manager.Player.Stats.FireRate = 0.2f;
        Manager.Player.Stats.ShotSpeed = 10f;
        Manager.Player.Stats.InvincibleTime =
        Manager.Player.Stats.ProjectileNum = _bulletProjectileNum;

        // 탄환 풀 생성
        _waitTime = new WaitForSeconds(Manager.Player.Stats.FireRate);
        _bulletPool = new Stack<GameObject>(_poolSize);

        for (int i = 0; i < _poolSize; i++)
        {
            GameObject bullet = Instantiate(_bulletPrefab);
            bullet.GetComponent<Bullet>().returnPool = _bulletPool;
            bullet.SetActive(false);
            _bulletPool.Push(bullet);
        }
    }
    IEnumerator Fire()
    {
        if (Manager.Player.Stats.ProjectileNum == 1)
        {
            // 단일 발사
            GameObject instance = _bulletPool.Pop();
            instance.SetActive(true);
            instance.transform.position = _muzzlePosition.position;
            instance.GetComponent<Rigidbody>().AddForce(_muzzlePosition.forward * Manager.Player.Stats.ShotSpeed, ForceMode.Impulse);
        }
        else if (Manager.Player.Stats.ProjectileNum > 1)
        {
            // 다중 발사
            for (int i = 0; i < Manager.Player.Stats.ProjectileNum; i++)
            {
                float startAngle;
                float angleGrid;

                if (Manager.Player.Stats.ProjectileNum % 2 == 1)
                {
                    angleGrid = (24f + Manager.Player.Stats.ProjectileNum) / Manager.Player.Stats.ProjectileNum - 1;
                    startAngle = -(angleGrid * (Manager.Player.Stats.ProjectileNum - 1)) / 2;
                }
                else
                {
                    angleGrid = (14f + Manager.Player.Stats.ProjectileNum) / Manager.Player.Stats.ProjectileNum - 1;
                    startAngle = -(angleGrid * (Manager.Player.Stats.ProjectileNum - 1)) / 2;
                }

                GameObject instance = _bulletPool.Pop();
                instance.SetActive(true);
                instance.transform.position = _muzzlePosition.position;
                _muzzlePosition.transform.Rotate(0, startAngle + angleGrid * i, 0);
                instance.GetComponent<Rigidbody>().AddForce(_muzzlePosition.forward * Manager.Player.Stats.ShotSpeed, ForceMode.Impulse);
                _muzzlePosition.transform.Rotate(0, -(startAngle + angleGrid * i), 0);
            }
        }

        yield return _waitTime;
        _fireCoroutine = null;
    }

    IEnumerator Invincibility()
    {
        IsDamagable = false;
        yield return new WaitForSeconds(_invincibleTime);
        IsDamagable = true;
        _invincibleRoutine = null;
    }
}
