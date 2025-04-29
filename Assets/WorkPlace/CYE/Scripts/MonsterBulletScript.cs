using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBulletScript : MonoBehaviour
{
    public Stack<GameObject> ReturnPool;
    public float Lifespan;
    private Coroutine _coroutine = null;
    private void OnEnable()
    {
        if (_coroutine is null) {
            _coroutine = StartCoroutine(CheckLifespan());
        }
    }
    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Player")) {
            Deactivate();
        }
    }
    private void Deactivate() {
        gameObject.SetActive(false);
        ReturnPool.Push(gameObject);
    }
    private IEnumerator CheckLifespan() 
    {
        yield return new WaitForSeconds(Lifespan);
        if (gameObject.activeSelf) {
            Deactivate();
        }

    }
}
