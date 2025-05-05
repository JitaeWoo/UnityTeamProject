using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    protected float CoolDown;
    protected Coroutine CooldownCoroutine;
    protected WaitForSeconds CooldownDelay;
    public bool _isReady => CooldownCoroutine == null;

    public void Use()
    {
        if (_isReady)
        {
            ActivateSkill();
            CooldownCoroutine = StartCoroutine(StartCoolDown());
        }
    }

    protected abstract void ActivateSkill();

    protected IEnumerator StartCoolDown()
    {
        if(CooldownDelay == null)
        {
            CooldownDelay = new WaitForSeconds(CoolDown);
        }

        yield return CooldownDelay;
        CooldownCoroutine = null;
    }
}
