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

    public void Start()
    {
        SetSkillIcon();
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
}
