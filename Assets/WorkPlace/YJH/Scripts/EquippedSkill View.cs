using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EquippedSkillView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI skillNameText;
    [SerializeField] private TextMeshProUGUI skillInfoText;

    private void Start()
    {
        // Manager.Player.Skill�� ���� ���� ������ ��ų�� ������
        PlayerSkill.Skills currentSkill = Manager.Player.Skill;

        // ��ų ������ UI�� �ݿ�
        UpdateSkillInfo(currentSkill);
    }

    private void UpdateSkillInfo(PlayerSkill.Skills skill)
    {
        switch (skill)
        {
            case PlayerSkill.Skills.GrenadeThrow:
                skillNameText.text = "����";
                skillInfoText.text = "���� �� �ݰ濡 ������ ������ �������� �ش�.";
                break;
            case PlayerSkill.Skills.BeamShot:
                skillNameText.text = "��";
                skillInfoText.text = "���� ���� �� �������� �ش�.";
                break;
            case PlayerSkill.Skills.Heal:
                skillNameText.text = "ġ��";
                skillInfoText.text = "�÷��̾��� ü���� ���� �κ� ȸ���Ѵ�.";
                break;
            case PlayerSkill.Skills.ClearBullet:
                skillNameText.text = "ź�� ����";
                skillInfoText.text = "���� ���� �κ��� �� ����ü�� �����Ѵ�.";
                break;
            case PlayerSkill.Skills.Slash:
                skillNameText.text = "����";
                skillInfoText.text = "���� ��ä�� ������ ���� ���� �������� �ش�.";
                break;
        }
    }
}