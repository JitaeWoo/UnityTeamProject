using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUIHandler : MonoBehaviour
{
    public PlayerSkill.Skills currentSkill;

    public Image dashIcon;
    public Image mainSkillicon;

    public Sprite grenadeIcon;
    public Sprite beamShotIcon;
    public Sprite healIcon;
    public Sprite clearBulletIcon;
    public Sprite slashIcon;

    private Dictionary<PlayerSkill.Skills, float> skillCooldowns = new Dictionary<PlayerSkill.Skills, float>
    {
        { PlayerSkill.Skills.GrenadeThrow, 5f },
        { PlayerSkill.Skills.BeamShot, 10f },
        { PlayerSkill.Skills.Heal, 10f },
        { PlayerSkill.Skills.ClearBullet, 5f },
        { PlayerSkill.Skills.Slash, 6f },
    };

    private float skillCooldownTime;
    private float skillCooldownTimer;
    private bool isSkillCoolingDown = false;

    private float dashCooldownTime = 2f;
    private float dashCooldownTimer;
    private bool isDashCoolingDown = false;

    private void Start()
    {
        SetSkillIcon();
        mainSkillicon.fillAmount = 1f;
        dashIcon.fillAmount = 1f;
    }

    private void Update()
    {
        // 마우스 우클릭: 현재 스킬 발동
        if (Input.GetMouseButtonDown(1) && !isSkillCoolingDown)
        {
            ShowSkillCooltime();
        }

        // 스페이스바: 대시 발동
        if (Input.GetKeyDown(KeyCode.Space) && !isDashCoolingDown)
        {
            ShowDashCooltime();
        }

        // 스킬 쿨타임 진행
        if (isSkillCoolingDown)
        {
            skillCooldownTimer += Time.deltaTime;
            mainSkillicon.fillAmount = skillCooldownTimer / skillCooldownTime;

            if (skillCooldownTimer >= skillCooldownTime)
            {
                isSkillCoolingDown = false;
                mainSkillicon.fillAmount = 1f;
            }
        }

        // 대시 쿨타임 진행
        if (isDashCoolingDown)
        {
            dashCooldownTimer += Time.deltaTime;
            dashIcon.fillAmount = dashCooldownTimer / dashCooldownTime;

            if (dashCooldownTimer >= dashCooldownTime)
            {
                isDashCoolingDown = false;
                dashIcon.fillAmount = 1f;
            }
        }
    }

    public void SetSkillIcon()
    {
        currentSkill = Manager.Player.Skill;

        switch (currentSkill)
        {
            case PlayerSkill.Skills.GrenadeThrow:
                mainSkillicon.sprite = grenadeIcon;
                break;
            case PlayerSkill.Skills.BeamShot:
                mainSkillicon.sprite = beamShotIcon;
                break;
            case PlayerSkill.Skills.Heal:
                mainSkillicon.sprite = healIcon;
                break;
            case PlayerSkill.Skills.ClearBullet:
                mainSkillicon.sprite = clearBulletIcon;
                break;
            case PlayerSkill.Skills.Slash:
                mainSkillicon.sprite = slashIcon;
                break;
        }
    }

    public void ShowSkillCooltime()
    {
        if (skillCooldowns.TryGetValue(currentSkill, out skillCooldownTime))
        {
            isSkillCoolingDown = true;
            skillCooldownTimer = 0f;
            mainSkillicon.fillAmount = 0f;
        }
    }

    public void ShowDashCooltime()
    {
        isDashCoolingDown = true;
        dashCooldownTimer = 0f;
        dashIcon.fillAmount = 0f;
    }
}
