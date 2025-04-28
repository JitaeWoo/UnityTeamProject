using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

enum MonsterType { 
    Melee, Range, Elite
}

public class MonsterController : MonoBehaviour, IDamagable
{
    #region > Variables

    #region >> Game object variables
    // ������ object
    [SerializeField] private GameObject _targetObject;
    // ����ü(�߻�ü) object
    [SerializeField] private GameObject _projectileObject;
    #endregion

    #region >> Private variables
    // �̵� �ӵ�
    [SerializeField] private float _moveSpeed;
    // _targetObject�� ���� ���� ����
    [SerializeField] private float _detectRange;
    private bool _isDetected;
    private LayerMask _detectLayer;
    // �� object�� rigidbody
    private Rigidbody _rigidbody;
    // ��� �ִϸ��̼� ����
    private float _dyingAnimationTime;
    // �浹 ����
    private bool _isCollide;
    // �߰� �浹 ����
    private float _collideRange = 1f;
    private Vector3 _targetDirection;
    // �浹�� �������� ������ϴ� object layer
    private LayerMask _blockMovementLayer;
    #endregion

    #region >> Public variables
    public int MonsterHp;
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
        // 250428 ���� �浹�� ���ߴ� ����� OnCollision~ �Լ��� ó���ϰ� ������, ���� ������ collider�� ����� ��츦 ����Ͽ� �� ������ �߰� �浹 ������ �߰��Ͽ� �浹 ���θ� �Ǵ��Ѵ�.
        DetectTarget();
        DetectCollide();
        if (_isDetected && !_isCollide)
        {
            FollowTarget();
        }
    }

    private void OnDrawGizmos()
    {
        // target object Ž������ ǥ��
        if(_isDetected){ Gizmos.color = Color.blue; } else { Gizmos.color = Color.green; }
            Gizmos.DrawWireSphere(transform.position, _detectRange);

        // �߰� �浹 ���� ǥ��
        if (_isCollide) { Gizmos.color = Color.red; } else { Gizmos.color = Color.yellow; }
        Gizmos.DrawRay(transform.position, _targetDirection);
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
    }

    public void TakeHit(int attackPoint) 
    {
        if (attackPoint >= MonsterHp)
        {
            MonsterHp = 0;
            // -> activate dying animation
            Die();
        }
        else 
        {
            MonsterHp -= attackPoint;
            // -> take damage animation variable change
        }
    }

    private void Die() 
    {
        Destroy(this, _dyingAnimationTime);
    }

    private void DetectTarget() {
        Debug.Log($"{_detectLayer}");
        if (Physics.OverlapSphere(transform.position, _detectRange, _detectLayer).Length > 0)
        {
            _isDetected = true;
        }
        else
        {
            _isDetected = false;
        }
    }

    private void FollowTarget() 
    {
        // rotate
        Vector3 lookPosition = new Vector3(_targetObject.transform.position.x, transform.position.y, _targetObject.transform.position.z);
        transform.LookAt(lookPosition);
        // move
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
    #endregion
}