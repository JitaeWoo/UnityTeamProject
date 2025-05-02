using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProjectileScript : MonoBehaviour
{
    #region > Variables

    #region >> Private variables
    private Stack<GameObject> _returnPool;
    private float _lifespan;
    private int _damage;
    private Coroutine _coroutine;
    #endregion

    #region >> Properties
    public Stack<GameObject> ReturnPool { get { return _returnPool; } set { _returnPool = value; } }
    public float Lifespan { get { return _lifespan; } set { _lifespan = value; } }
    public int Damage { get { return _damage; } set { _damage = value; } }
    #endregion

    #endregion



    #region > Unity message functions
    private void Awake()
    {
        _coroutine = null;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log($"{(collision.gameObject.name)}");
            //collision.gameObject.GetComponent<IDamagable>()?.TakeHit(_damage);
            //Deactivate();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log($"{(other.gameObject.name)}");
            //other.gameObject.GetComponent<IDamagable>()?.TakeHit(_damage);
            //Deactivate();
        }
    }
    #endregion



    #region > Custom functions
    public void Activate()
    {
        gameObject.SetActive(true);
        if (_coroutine is null)
        {
            _coroutine = StartCoroutine(CheckLifespan());
        }
    }

    public void Deactivate()
    {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.SetActive(false);
        if (_coroutine is not null) 
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
        _returnPool.Push(gameObject);
    }
    private IEnumerator CheckLifespan() 
    {
        yield return new WaitForSeconds(_lifespan);
        if (gameObject.activeSelf) {
            Deactivate();
        }
    }
    #endregion
}
