using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] private int _pierceNum;
    [SerializeField] private float _shotSpeed;
    [SerializeField] private float _fireRate;

    // �÷��̾� ����
    private Camera _mainCamera;
    private Vector3 _moveDirection;
    private bool IsDamagable = true;
    private Coroutine _invincibleRoutine;
    private PlayerAnimation _playerAnimation;
    private bool _isDied;

    // źȯ Ǯ
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

    void Update()
    {
        PlayerAttack();
    }

    private void PlayerMovement()
    {
        if (!_isDied)
        {
            // �÷��̾� �̵� �Է�, WASD �̵� / ���̽�ƽ �̴���. ���� Horizontal, Vertical�� ���� �� ���� ����
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
    }

    private void PlayerAim()
    {
        if (!_isDied)
        {
            // �÷��̾� ���콺 ����
        Vector3 lookPos = Input.mousePosition;
        lookPos.z = _mainCamera.transform.position.y - _player.transform.position.y;
        lookPos = _mainCamera.ScreenToWorldPoint(lookPos);
            _player.transform.forward = lookPos - transform.position;
        }
    }

    private void PlayerAttack()
    {
        if (!_isDied)
        {
            // �÷��̾� źȯ �߻�
            if (Input.GetMouseButton(0))
            {
                if (IsReadyFire)
                {
                    _playerAnimation.AttackAnimation();
                    _fireCoroutine = StartCoroutine(Fire());
                }
            }
            else
            {
                _playerAnimation.StopAttack();
            }
        }
    }

    void Died()
    {
        // ����� �ı�
        _playerAnimation.StopAttack();
        _isDied = true;
        _playerAnimation.DyingAnimation();
        Destroy(_player.gameObject, 1.5f);
    }

    public void TakeHit(int damage)
    {
        // �÷��̾� ������, �����ð������� ���ظ� ���� ����
        if (Manager.Player.Stats.CurHp > 0)
        {
            if (IsDamagable)
            Manager.Player.Stats.CurHp -= damage;
            if (_invincibleRoutine == null)
                _invincibleRoutine = StartCoroutine(Invincibility());
        }
    }

    public Vector3 GetMoveDirection()
    {
        return _moveDirection;
    }

    public Transform GetMuzzleTransform()
    {
        return _muzzlePosition;
    }

    private void OnDestroy()
    {
        Manager.Player.OnDied -= Died;
    }

    void Init()
    {
        // �⺻ �ʱ�ȭ ����
        _mainCamera = Camera.main;
        Manager.Player.OnDied += Died;
        _playerAnimation = GetComponent<PlayerAnimation>();
        _isDied = false;

        // �Ʒ��� �ӽ� �׽�Ʈ��
        Manager.Player.Stats.FireRate = _fireRate;
        Manager.Player.Stats.ShotSpeed = _shotSpeed;
        Manager.Player.Stats.InvincibleTime = _invincibleTime;
        Manager.Player.Stats.ProjectileNum = _bulletProjectileNum;
        Manager.Player.Stats.PierceNum = _pierceNum;

        // źȯ Ǯ ����
        _waitTime = new WaitForSeconds(Manager.Player.Stats.FireRate);
        _bulletPool = new Stack<GameObject>(_poolSize);

        for (int i = 0; i < _poolSize; i++)
        {
            GameObject bullet = Instantiate(_bulletPrefab);
            bullet.transform.parent = this.transform;
            bullet.GetComponent<Bullet>().returnPool = _bulletPool;
            bullet.SetActive(false);
            _bulletPool.Push(bullet);
        }
    }
    IEnumerator Fire()
    {
        if (Manager.Player.Stats.ProjectileNum == 1)
        {
            // ���� �߻�
            GameObject instance = _bulletPool.Pop();
            instance.transform.SetParent(null);
            instance.SetActive(true);
            instance.GetComponent<Bullet>().PierceNum = Manager.Player.Stats.PierceNum;
            instance.transform.position = _muzzlePosition.position;
            instance.GetComponent<Rigidbody>().AddForce(_muzzlePosition.forward * Manager.Player.Stats.ShotSpeed, ForceMode.Impulse);
        }
        else if (Manager.Player.Stats.ProjectileNum > 1)
        {
            // ���� �߻�
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
                instance.transform.SetParent(null);
                instance.SetActive(true);
                instance.GetComponent<Bullet>().PierceNum = Manager.Player.Stats.PierceNum;
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
        // ���� �ð� ����
        IsDamagable = false;
        yield return new WaitForSeconds(Manager.Player.Stats.InvincibleTime);
        IsDamagable = true;
        _invincibleRoutine = null;
    }
}
