using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public enum MonsterType { 
    Melee, Range, Elite
}

public class MonsterController : MonoBehaviour, IDamagable
{
    #region > Variables

    #region >> Serialized variables
    public int Hp;
    public MonsterType Type;
    public int Damage;
    public UnityEvent OnDied;
    //[SerializeField] private GameObject _targetObject;
    //[SerializeField] private GameObject _projectileObject;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _detectRange;
    [SerializeField] private float _attackRange;
    //[SerializeField] private int _projectilePoolSize;
    //[SerializeField] private Transform _muzzlePoint;
    [SerializeField] private MeleeAttackInfo _meleeAttackInfo;
    [SerializeField] private RangeAttackInfo _rangeAttackInfo;
    #endregion

    #region >> Private variables
    // target object 
    private GameObject _targetObject;
    // target object 탐지여부
    private bool _isDetected;
    // target object layer
    private LayerMask _detectLayer;
    // 현 object의 rigidbody
    private Rigidbody _rigidbody;
    // 사망 애니메이션 길이
    private float _dyingAnimationTime;
    // 충돌 여부
    private bool _isCollide;
    // 추가 충돌 범위
    private float _collideRange = 1f; // 추후 직렬화 예정
    // target object 방향
    private Vector3 _targetDirection;
    // 충돌시 움직임을 멈춰야하는 object layer
    private LayerMask _blockMovementLayer;
    // monster attack 객체
    private MonsterAttack _monsterAttack;
    // monster attack coroutine
    private Coroutine _attackRoutine;
    // 투사체 object pool
    private Stack<GameObject> _projectilePool;
    private bool _isAttackable;
    #endregion

    #endregion

    // ===== //

    #region > Unity message functions
    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        //_detectLayer = 1 << _targetObject.layer;
    }
    private void OnEnable()
    {
        _targetObject = Manager.Player.Player;
        _detectLayer = 1 << _targetObject.layer;
    }

    private void Update()
    {
        if(_targetObject != null && Manager.Player.Stats.CurHp > 0)
        {
            DetectTarget();
            DetectCollide();
            CheckAttackable();
            if (_isDetected && !_isCollide)
            {
                LookTarget();
                FollowTarget();
                if (_isAttackable) {
                    Attack();
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // target object 탐지범위 표시
        if(_isDetected){ Gizmos.color = Color.blue; } else { Gizmos.color = Color.green; }
        Gizmos.DrawWireSphere(transform.position, _detectRange);

        // 추가 충돌 영역 표시
        if (_isCollide) { Gizmos.color = Color.red; } else { Gizmos.color = Color.yellow; }
        Gizmos.DrawRay(transform.position, _targetDirection);

        // 공격 영역 표시
        if (_isAttackable) { Gizmos.color = Color.red; } else { Gizmos.color = Color.yellow; }
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
    #endregion

    // ===== //

    #region > Custom functions
    private void Init()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _dyingAnimationTime = 3f;
        _isCollide = false;
        _blockMovementLayer = LayerMask.GetMask("Player", "Wall");
        _isAttackable = false;
        if (Type == MonsterType.Range) 
        {
            _projectilePool = new Stack<GameObject>(_rangeAttackInfo.ProjectileTotalCount);
            for (int cnt = 0; cnt < _rangeAttackInfo.ProjectileTotalCount; cnt++)
            {
                GameObject instant = Instantiate(_rangeAttackInfo.Projectile);
                instant.GetComponent<MonsterProjectileScript>().ReturnPool = _projectilePool;
                instant.GetComponent<MonsterProjectileScript>().Lifespan = 3f;
                instant.SetActive(false);
                _projectilePool.Push(instant);
            }
        }
        _monsterAttack = new MonsterAttack(_rigidbody, _meleeAttackInfo, _rangeAttackInfo);
    }

    public void TakeHit(int attackPoint) 
    {
        if (attackPoint >= Hp)
        {
            Hp = 0;
            // -> activate dying animation
            Die();
        }
        else 
        {
            Hp -= attackPoint;
            // -> take damage animation variable change
        }
    }

    private void Die() 
    {
        OnDied?.Invoke();
        Destroy(gameObject, _dyingAnimationTime);
    }

    private void DetectTarget() {
        if (Physics.OverlapSphere(transform.position, _detectRange, _detectLayer).Length > 0)
        {
            _isDetected = true;
        }
        else
        {
            _isDetected = false;
        }
    }

    private void LookTarget()
    {
        Vector3 lookPosition = new Vector3(_targetObject.transform.position.x, transform.position.y, _targetObject.transform.position.z);
        transform.LookAt(lookPosition);
    }
    private void FollowTarget()
    {
        Vector3 targetPosition = new Vector3(_targetObject.transform.position.x, transform.position.y, _targetObject.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _moveSpeed * Time.deltaTime);
    }

    private void DetectCollide()
    {
        _targetDirection = new Vector3((_targetObject.transform.position.x - transform.position.x), transform.position.y, (_targetObject.transform.position.z - transform.position.z));
        if (Physics.Raycast(transform.position, _targetDirection, _collideRange, _blockMovementLayer))
        {
            _isCollide = true;
        }
        else
        {
            _isCollide = false;
        }
    }

    private void CheckAttackable()
    {
        _targetDirection = new Vector3((_targetObject.transform.position.x - transform.position.x), transform.position.y, (_targetObject.transform.position.z - transform.position.z));
        if (Physics.OverlapSphere(transform.position, _attackRange, _detectLayer).Length > 0)
        //if (Physics.Raycast(transform.position, _targetDirection, _attackRange, _detectLayer))
        {
            _isAttackable = true;
        }
        else
        {
            _isAttackable = false;
            if (_attackRoutine is not null)
            {
                StopCoroutine(_attackRoutine);
                _attackRoutine = null;
            }
        }
    }

    private void Attack()
    {
        //_targetDirection = new Vector3((_targetObject.transform.position.x - transform.position.x), transform.position.y, (_targetObject.transform.position.z - transform.position.z));
        //if (Physics.Raycast(transform.position, _targetDirection, _attackRange, _detectLayer))
        //{
        //    if (_attackRoutine is null)
        //    {
        //        switch (Type)
        //        {
        //            case MonsterType.Melee:
        //                _attackRoutine = StartCoroutine(_monsterAttack.Dash());
        //                break;
        //            case MonsterType.Range:
        //                _attackRoutine = StartCoroutine(_monsterAttack.Shooting(_projectilePool));
        //                break;
        //            case MonsterType.Elite:
        //                _attackRoutine = StartCoroutine(_monsterAttack.Jump());
        //                break;
        //        }
        //    }
        //}
        //else 
        //{
        //    if (_attackRoutine is not null)
        //    {
        //        StopCoroutine(_attackRoutine);
        //        _attackRoutine = null;
        //    }
        //}
        if (_attackRoutine is null)
        {
            switch (Type)
            {
                case MonsterType.Melee:
                    _attackRoutine = StartCoroutine(_monsterAttack.Dash());
                    break;
                case MonsterType.Range:
                    _attackRoutine = StartCoroutine(_monsterAttack.Shooting(_projectilePool));
                    break;
                case MonsterType.Elite:
                    _attackRoutine = StartCoroutine(_monsterAttack.Jump());
                    break;
            }
        }
    }
    #endregion
}