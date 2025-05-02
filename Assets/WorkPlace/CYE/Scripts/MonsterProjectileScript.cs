using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProjectileScript : MonoBehaviour
{
    #region > Variables
    public Stack<GameObject> ReturnPool;
    public float Lifespan;
    public int Damage;
    private Coroutine _coroutine;
    #endregion



    #region > Unity message functions
    private void Awake()
    {
        _coroutine = null;
    }
    //private void OnCollisionEnter(Collision collision) 
    //{
    //    if (collision.gameObject.CompareTag("Player")) {
    //        collision.gameObject.GetComponent<IDamagable>()?.TakeHit(Damage);
    //        Deactivate();
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<IDamagable>()?.TakeHit(Damage);
            Deactivate();
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
        gameObject.SetActive(false);
        if (_coroutine is not null) 
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
        ReturnPool.Push(gameObject);
    }
    private IEnumerator CheckLifespan() 
    {
        yield return new WaitForSeconds(Lifespan);
        if (gameObject.activeSelf) {
            Deactivate();
        }
    }
    #endregion
}
