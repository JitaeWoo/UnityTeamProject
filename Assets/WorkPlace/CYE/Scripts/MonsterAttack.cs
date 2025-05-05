using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MeleeAttackInfo
{
    public float Term;
    public float Speed;
}

[System.Serializable]
public struct RangeAttackInfo 
{
    public float Term;
    public GameObject Projectile;
    public int ProjectileTotalCount;
    public float ProjectileSpeed;
    public Transform MuzzlePoint;
}

public enum AttackType 
{ 
    Dash, Jump, Shoot
}

public class MonsterAttack // : MonoBehaviour
{
    #region > Variables
    private Rigidbody _rigidbody;
    private MeleeAttackInfo _meleeAttackInfo;
    private RangeAttackInfo _rangeAttackInfo;
    #endregion



    #region > Constructor
    public MonsterAttack(Rigidbody rigid, MeleeAttackInfo meleeAttackInfo, RangeAttackInfo rangeAttackInfo) {
        _rigidbody = rigid;
        _meleeAttackInfo = meleeAttackInfo;
        _rangeAttackInfo = rangeAttackInfo;
    }
    #endregion



    #region > Custom functions
    public IEnumerator Dash() {
        while (true) {
            _rigidbody.AddForce(_rigidbody.transform.forward * _meleeAttackInfo.Speed, ForceMode.Impulse);
            yield return new WaitForSeconds(_meleeAttackInfo.Term);
        }
    }

    public IEnumerator Jump()
    {
        while (true)
        {
            _rigidbody.AddForce(_rigidbody.transform.up * _meleeAttackInfo.Speed, ForceMode.Impulse);
            yield return new WaitForSeconds(_meleeAttackInfo.Term);
        }
    }

    public IEnumerator Shooting(Stack<GameObject> projectiles)
    {
        while (true)
        {
            if (projectiles.TryPop(out GameObject projectile)) 
            {
                projectile.transform.position = _rangeAttackInfo.MuzzlePoint.position;
                projectile.transform.forward = _rangeAttackInfo.MuzzlePoint.forward;
                projectile.GetComponent<MonsterProjectileScript>().Activate();
                projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * _rangeAttackInfo.ProjectileSpeed, ForceMode.Impulse);
            }
            yield return new WaitForSeconds(_rangeAttackInfo.Term);
        }
    }
    #endregion
}
