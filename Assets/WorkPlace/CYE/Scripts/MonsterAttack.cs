using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MeleeAttackInfo
{
    public float Term;
    public float Speed;
}

public struct RangeAttackInfo 
{
    public float Term;
    public GameObject Projectile;
    public int ProjectileTotalCount;
    public float ProjectileSpeed;
    public Transform MuzzlePoint;
}

public class MonsterAttack // : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Transform _muzzlePoint;

    public MonsterAttack(Rigidbody rigid, Transform muzzlePoint) {
        _rigidbody = rigid;
        _muzzlePoint = muzzlePoint;
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

    public IEnumerator Shooting(Stack<GameObject> projectiles)
    {
        while (true)
        {
            if (projectiles.TryPop(out GameObject projectile)) 
            {
                Debug.Log("Fire");
                projectile.SetActive(true);
                projectile.transform.position = _muzzlePoint.position;
                projectile.transform.forward = _muzzlePoint.forward;
                projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * 3f, ForceMode.Impulse);
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
