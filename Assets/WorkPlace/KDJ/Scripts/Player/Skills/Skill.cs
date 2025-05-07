using System.Collections;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public float CoolDown;
    public float SkillMotionDelay;
    protected Coroutine CooldownCoroutine;
    protected Coroutine DelayCoroutine;
    protected WaitForSeconds CooldownDelay;
    protected WaitForSeconds SkillDelayTime;
    protected PlayerAnimation _playerAnimation;

    public bool _isReady => CooldownCoroutine == null;

    public void Use()
    {
        if (_isReady && DelayCoroutine == null)
        {
            if (SkillMotionDelay > 0)
                _playerAnimation.SkillAnimation();
            DelayCoroutine = StartCoroutine(SkillDelay());
        }
    }

    protected abstract void ActivateSkill();

    protected IEnumerator StartCoolDown()
    {
        if (CooldownDelay == null)
        {
            CooldownDelay = new WaitForSeconds(CoolDown);
        }

        yield return CooldownDelay;
        CooldownCoroutine = null;
    }

    protected IEnumerator SkillDelay()
    {
        if (SkillDelayTime == null)
        {
            SkillDelayTime = new WaitForSeconds(SkillMotionDelay);
        }
        yield return SkillDelayTime;
        ActivateSkill();
        CooldownCoroutine = StartCoroutine(StartCoolDown());
        DelayCoroutine = null;
    }
}
