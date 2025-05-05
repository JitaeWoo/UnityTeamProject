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
                break;
            case Skills.BeamShot:
                _skill = gameObject.AddComponent<BeamShot>();
                break;
            case Skills.Heal:
                _skill = gameObject.AddComponent<Heal>();
                break;
            case Skills.ClearBullet:
                _skill = gameObject.AddComponent<ClearBullet>();
                break;
            case Skills.Slash:
                _skill = gameObject.AddComponent<Slash>();
                break;
        }
    }

    IEnumerator SkillAniDelay()
    {
        yield return new WaitForSeconds(0.60f);
        _skill.Use();
        _skillDelay = null;
    }
}
