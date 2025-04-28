using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] int _poolSize;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _muzzlePosition;

    // 플레이어 움직임 관련
    private Camera _mainCamera;
    public PlayerStats PlayerStats;
    private Vector3 _moveDirection;

    //탄환
    private Stack<GameObject> _bulletPool;
    private WaitForSeconds _waitTime;
    private Coroutine _fireCoroutine;

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

        _player.GetComponent<Rigidbody>().MovePosition(_player.transform.position + _moveDirection * PlayerStats.Speed * Time.fixedDeltaTime);
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
        if (Input.GetMouseButtonDown(0))
        {
            if (_fireCoroutine == null)
            {
                _fireCoroutine = StartCoroutine(Fire());
            }
            Debug.Log("Fire!!");
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (_fireCoroutine != null)
            {
                StopCoroutine(_fireCoroutine);
                _fireCoroutine = null;
            }
            Debug.Log("Stop Fire!!");
        }
    }

    void Init()
    {
        // 기본 초기화 과정
        _mainCamera = Camera.main;
        PlayerStats.Speed = 5f;
        PlayerStats.FireRate = 0.5f;
        PlayerStats.ShotSpeed = 10f;

        // 탄환 풀 생성
        _waitTime = new WaitForSeconds(_player.GetComponent<PlayerController>().PlayerStats.FireRate);
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
        while(true)
        {
            GameObject instance = _bulletPool.Pop();
            instance.SetActive(true);
            instance.transform.position = _muzzlePosition.position;
            instance.GetComponent<Rigidbody>().AddForce(_muzzlePosition.forward * PlayerStats.ShotSpeed, ForceMode.Impulse);
            yield return _waitTime;
        }
    }
}
