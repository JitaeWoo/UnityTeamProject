using System.Collections;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public enum Skills
    {
        GrenadeThrow, BeamShot, Heal, ClearBullet, Slash, size
    }

    [SerializeField] Skills skill;
    private Skill _skill;
    private Skill _utilitySkill;
    private PlayerAnimation _playerAnimation;
    private Coroutine _skillDelay;
    private int _skillNum;

    private void Awake()
    {
        // EquipSkill(Manager.Player.Skill);
        EquipSkill(skill); // 테스트용. 끝나면 위로 교체
        _utilitySkill = gameObject.AddComponent<Dash>();
        _playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (_skill._isReady)
                _playerAnimation.SkillAnimation();

            if (_skillDelay == null)
                _skillDelay = StartCoroutine(SkillAniDelay());

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _utilitySkill.Use();
        }
    }

    public void EquipSkill(Skills skill)
    {
        if (_skill != null)
        {
            Destroy(_skill);
            _skill = null;
        }

        switch (skill)
        {
            case Skills.GrenadeThrow:
                _skill = gameObject.AddComponent<GrenadeThrow>();
                _skillNum = 1;
                break;
            case Skills.BeamShot:
                _skill = gameObject.AddComponent<BeamShot>();
                _skillNum = 2;
                break;
            case Skills.Heal:
                _skill = gameObject.AddComponent<Heal>();
                _skillNum = 3;
                break;
            case Skills.ClearBullet:
                _skill = gameObject.AddComponent<ClearBullet>();
                _skillNum = 4;
                break;
            case Skills.Slash:
                _skill = gameObject.AddComponent<Slash>();
                _skillNum = 5;
                break;
        }
    }

    IEnumerator SkillAniDelay()
    {
        if (_skillNum == 4)
            yield return new WaitForSeconds(0.01f);
        else
            yield return new WaitForSeconds(0.65f);
        _skill.Use();
        _skillDelay = null;
    }
}
