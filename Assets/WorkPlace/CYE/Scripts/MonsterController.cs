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
    // 추적할 object
    [SerializeField] private GameObject _targetObject;
    // 투사체(발사체) object
    [SerializeField] private GameObject _projectileObject;
    // 이동 속도
    [SerializeField] private float _moveSpeed;
    // _targetObject에 대한 추적 범위
    [SerializeField] private float _detectRange;
    [SerializeField] private float _attackRange;
    [SerializeField] private int _projectilePoolSize;
    [SerializeField] private Transform _muzzlePoint;
    #endregion

    #region >> Private variables
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
    private float _collideRange = 1f;
    private Vector3 _targetDirection;
    // 충돌시 움직임을 멈춰야하는 object layer
    private LayerMask _blockMovementLayer;
    //private UnityEvent _attack;
    private MonsterAttack _monsterAttack;

    private Coroutine _attackRoutine;

    private Stack<GameObject> _projectilePool;
    #endregion

    #endregion

    // ===== //

    #region > Unity message functions
    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        // 250428 현재 충돌시 멈추는 방식을 OnCollision~ 함수로 처리하고 있으나, 향후 복잡한 collider를 사용할 경우를 대비하여 원 형태의 추가 충돌 범위를 추가하여 충돌 여부를 판단한다.
        DetectTarget();
        DetectCollide();
        if (_isDetected && !_isCollide)
        {
            LookTarget();
            FollowTarget();
            Attack();
        }
    }

    private void OnDrawGizmos()
    {
        // target object 탐지범위 표시
        if(_isDetected){ Gizmos.color = Color.blue; } else { Gizmos.color = Color.green; }
        Gizmos.DrawWireSphere(transform.position, _detectRange);

        // 추가 충돌 영역 표시
        if (_isCollide) { Gizmos.color = Color.red; } else { Gizmos.color = Color.yellow; }
        Gizmos.DrawRay(transform.position, _targetDirection);

        // 공격 영역 표시
        //if (_isCollide) { Gizmos.color = Color.red; } else { Gizmos.color = Color.yellow; }
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.CompareTag("Player"))
    //    {
    //        _isCollide = true;
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.transform.CompareTag("Player"))
    //    {
    //        _isCollide = false;
    //    }
    //}
    #endregion

    // ===== //

    #region > Custom functions
    private void Init()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
        _dyingAnimationTime = 3f;
        _detectLayer = 1 << _targetObject.layer;
        _isCollide = false;
        _blockMovementLayer = LayerMask.GetMask("Player", "Wall");
        //_attack = new UnityEvent();

        _projectilePool = new Stack<GameObject>(_projectilePoolSize);
        for (int cnt = 0; cnt < _projectilePoolSize; cnt++) 
        {
            GameObject instant = Instantiate(_projectileObject);
            instant.GetComponent<MonsterBulletScript>().ReturnPool = _projectilePool;
            instant.GetComponent<MonsterBulletScript>().Lifespan = 3f;
            instant.SetActive(false);
            _projectilePool.Push(instant);
        }
        _monsterAttack = new MonsterAttack(_rigidbody, _muzzlePoint);
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
        Destroy(this, _dyingAnimationTime);
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
        transform.position = Vector3.MoveTowards(transform.position, _targetObject.transform.position, _moveSpeed * Time.deltaTime);
    }

    private void DetectCollide()
    {
        _targetDirection = _targetObject.transform.position - transform.position;
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
        _targetDirection = _targetObject.transform.position - transform.position;
        //if (Physics.OverlapSphere(transform.position, _attackRange, _detectLayer).Length > 0)
        if (Physics.Raycast(transform.position, _targetDirection, _attackRange, _detectLayer))
        {

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
        else
        {
            if (_attackRoutine is not null)
            {
                StopCoroutine(_attackRoutine);
                _attackRoutine = null;
            }
        }
    }

    private void Attack()
    {
        _targetDirection = _targetObject.transform.position - transform.position;
        if (Physics.Raycast(transform.position, _targetDirection, _attackRange, _detectLayer))
        {
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
        else 
        {
            if (_attackRoutine is not null)
            {
                StopCoroutine(_attackRoutine);
                _attackRoutine = null;
            }
        }
    }
    #endregion
}