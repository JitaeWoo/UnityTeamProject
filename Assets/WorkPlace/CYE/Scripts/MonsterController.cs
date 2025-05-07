using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public enum MonsterType { 
    Melee, Range, Flying, Elite
}

public class MonsterController : MonoBehaviour, IDamagable
{
    #region > Variables

    #region >> Properties
    public MonsterType Type { get { return _type; } private set { _type = value; } }
    public int CurHp { get { return _curHp; } private set { _curHp = value; } }
    public int MaxHp { get { return _maxHp; } private set { _maxHp = value; } }
    public int Damage { get { return _damage; } private set { _damage = value; } }
    public bool IsMiniBoss { get { return _isMiniBoss; } private set { _isMiniBoss = value; } }
    #endregion

    #region >> Public variables
    [HideInInspector] public UnityEvent OnDied;
    [HideInInspector] public UnityEvent OnChangeCurHp;
    #endregion

    #region >> Serialized variables
    [SerializeField] private MonsterType _type;
    [SerializeField] private int _maxHp;
    [SerializeField] private int _damage;
    [SerializeField] private bool _isMiniBoss;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _detectRange;
    [SerializeField] private float _attackRange;
    [SerializeField] private AttackType[] _attackTypes;
    [SerializeField] private MeleeAttackInfo _meleeAttackInfo;
    [SerializeField] private RangeAttackInfo _rangeAttackInfo;
    #endregion

    #region >> Private variables
    // 현재 hp
    private int _curHp;
    // target object 
    private GameObject _targetObject;
    // target object 탐지여부
    private bool _isDetected;
    // target object layer
    private LayerMask _detectLayer;
    // 현 object의 rigidbody
    private Rigidbody _rigidbody;
    private Animator _animator;
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
    // 공격 가능 여부
    private bool _isAttackable;
    // 사망 여부
    private bool _isDied;
    private bool _isDamaged;
    private Coroutine _animatorControllRoutine;
    #endregion

    #endregion



    #region > Unity message functions
    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        _targetObject = Manager.Player.Player;
        _detectLayer = 1 << _targetObject.layer;
    }

    private void Update()
    {
        if (_targetObject != null && Manager.Player.Stats.CurHp > 0)
        {
            DetectTarget();
            DetectCollide();
            CheckAttackable();
            if (_isDetected && !_isCollide && !_isDied)
            {
                LookTarget();
                FollowTarget();
                if (_isAttackable)
                {
                    Attack();
                }
            }
        }
        else 
        {
            _isDetected = false;
            _isCollide = false;
            _isAttackable = false;
            if (_attackRoutine is not null)
            {
                StopCoroutine(_attackRoutine);
                _attackRoutine = null;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && Type != MonsterType.Range && !_isDied)
        {
            collision.gameObject.GetComponentInParent<IDamagable>()?.TakeHit(Damage);
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



    #region > Custom functions
    private void Init()
    {
        _curHp = _maxHp;
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _animator = gameObject.GetComponent<Animator>();
        _animatorControllRoutine = null;
        _isDied = false;
        _dyingAnimationTime = 3.0f;
        _isCollide = false;
        _blockMovementLayer = LayerMask.GetMask("Player", "Wall");
        _isAttackable = false;
        _isDamaged = false;
        if (Type == MonsterType.Range) 
        {
            _projectilePool = new Stack<GameObject>(_rangeAttackInfo.ProjectileTotalCount);
            for (int cnt = 0; cnt < _rangeAttackInfo.ProjectileTotalCount; cnt++)
            {
                GameObject instant = Instantiate(_rangeAttackInfo.Projectile);
                instant.GetComponent<MonsterProjectileScript>().ReturnPool = _projectilePool;
                instant.GetComponent<MonsterProjectileScript>().Lifespan = 3f;
                instant.GetComponent<MonsterProjectileScript>().Damage = Damage;
                instant.SetActive(false);
                _projectilePool.Push(instant);
            }
        }
        _monsterAttack = new MonsterAttack(_rigidbody, _meleeAttackInfo, _rangeAttackInfo);
    }

    public void TakeHit(int attackPoint) 
    {
        if (!_isDied)
        {
            if (attackPoint >= CurHp)
            {
                CurHp = 0;
                Die();
            }
            else
            {
                CurHp -= attackPoint;
                if (!_isDamaged)
                {
                    _isDamaged = true;
                    _animator.SetBool("GetDamaged", _isDamaged);
                    if (_animatorControllRoutine == null)
                    {
                        _animatorControllRoutine = StartCoroutine(ReleaseDamaged());
                    }
                }
            }
            OnChangeCurHp?.Invoke();
        }
    }
    private IEnumerator ReleaseDamaged() {
        yield return new WaitForSeconds(0.8f);
        _isDamaged = false;
        _animator.SetBool("GetDamaged", _isDamaged);
        if (_animatorControllRoutine != null)
        {
            StopCoroutine(_animatorControllRoutine);
            _animatorControllRoutine = null;
        }
    }

    private void Die()
    {
        _isDetected = false;
        _isCollide = false;
        _isAttackable = false;
        _isDamaged = false;
        _isDied = true;

        if (_attackRoutine is not null)
        {
            StopCoroutine(_attackRoutine);
            _attackRoutine = null;
        }
        if (_animatorControllRoutine != null)
        {
            StopCoroutine(_animatorControllRoutine);
            _animatorControllRoutine = null;
        }

        _animator.SetBool("IsDetected", _isDetected);
        _animator.SetBool("IsAttacking", _isAttackable);
        _animator.SetBool("GetDamaged", _isDamaged);
        _animator.SetBool("IsDying", _isDied);

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
        _animator.SetBool("IsDetected", _isDetected);
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
        _animator.SetBool("IsAttacking", _isAttackable);
    }

    private void Attack()
    {
        if (_attackRoutine is null)
        {
            foreach (AttackType attackType in _attackTypes)
            {
                switch (attackType)
                {
                    case AttackType.Dash:
                        _attackRoutine = StartCoroutine(_monsterAttack.Dash());
                        break;
                    case AttackType.Shoot:
                        _attackRoutine = StartCoroutine(_monsterAttack.Shooting(_projectilePool));
                        break;
                    case AttackType.Jump:
                        _attackRoutine = StartCoroutine(_monsterAttack.Jump());
                        break;
                }
            }
        }
    }
    #endregion
}