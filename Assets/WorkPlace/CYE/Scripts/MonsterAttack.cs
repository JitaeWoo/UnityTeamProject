using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack // : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public MonsterAttack(Rigidbody rigid) {
        _rigidbody = rigid;
    }

    //public void Dash() 
    //{

    //}
    public void SetAttack() 
    {
    
    }
    public void Activate() 
    {
        //_attackRoutine = StartCoroutine(Dash());
    }
    public void Deactivate() 
    { 
    
    }
    public IEnumerator Dash() {
        while (true) {
            _rigidbody.AddForce(_rigidbody.transform.forward * 3f, ForceMode.Impulse);
            yield return new WaitForSeconds(1f);
        }
    }

    public IEnumerator Jump()
    {
        while (true)
        {
            _rigidbody.AddForce(_rigidbody.transform.up * 3f, ForceMode.Impulse);
            yield return new WaitForSeconds(1f);
        }

    }

    public IEnumerator Shooting(GameObject _bullet)
    {
        while (true)
        {
            //_rigidbody.AddForce(_rigidbody.transform.up * 3f, ForceMode.Impulse);
            Debug.Log("Shoot");
            yield return new WaitForSeconds(1f);
        }
    }
}
