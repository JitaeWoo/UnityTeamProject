using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EquippedSkill : MonoBehaviour
{
    private PlayerSkill skillComponent;  // �ʵ� �ʱ�ȭ ����
    public TMP_Text skillNameText;

    private void Awake()
    {
        // Awake���� skillComponent �ʱ�ȭ
        skillComponent = Manager.Player.GetComponent<PlayerSkill>();

        // ������ ��ų�� �̸��� �����ͼ� �ؽ�Ʈ�� ǥ��
        DisplaySkillName();
    }

    public void DisplaySkillName()
    {
        // ������ ��ų�� �̸��� ������
        string displayName = GetSkillDisplayName(skillComponent.EquippedSkillType);

        // �ؽ�Ʈ UI�� ǥ��
        skillNameText.text = displayName;
    }

    string GetSkillDisplayName(PlayerSkill.Skills skill)
    {
        switch (skill)
        {
            case PlayerSkill.Skills.GrenadeThrow: return "����";
            case PlayerSkill.Skills.BeamShot: return "��";
            case PlayerSkill.Skills.Heal: return "ġ��";
            case PlayerSkill.Skills.ClearBullet: return "ź�� ����";
            case PlayerSkill.Skills.Slash: return "����";
            default: return "�� �� ����";
        }
    }
}
