using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillSelect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _skillName;
    [SerializeField] private TextMeshProUGUI _skillInfo;
    private PlayerSkill.Skills _selected;
    private int _selectedIndex;

    private void Awake()
    {
        _selectedIndex = (int)_selected;
    }

    void Start()
    {
        PrintSkillInfo(_selected);
    }

    private void PrintSkillInfo(PlayerSkill.Skills skill)
    {
        switch (skill)
        {
            case PlayerSkill.Skills.GrenadeThrow:
                _skillName.text = "����";
                _skillInfo.text = "���� �� �ݰ濡 ������ ������ �������� �ش�.";
                break;
            case PlayerSkill.Skills.BeamShot:
                _skillName.text = "��";
                _skillInfo.text = "���� ���� �� �������� �ش�.";
                break;
            case PlayerSkill.Skills.Heal:
                _skillName.text = "ġ��";
                _skillInfo.text = "�÷��̾��� ü���� ���� �κ� ȸ���Ѵ�.";
                break;
            case PlayerSkill.Skills.ClearBullet:
                _skillName.text = "ź�� ����";
                _skillInfo.text = "���� ���� �κ��� �� ����ü�� �����Ѵ�.";
                break;
            case PlayerSkill.Skills.Slash:
                _skillName.text = "����";
                _skillInfo.text = "���� ��ä�� ������ ���� ���� �������� �ش�.";
                break;
        }
    }

    public void SkillChange(bool isLeft)
    {
        if (isLeft)
        {
            _selectedIndex--;
            if(_selectedIndex < 0)
            {
                _selectedIndex = (int)PlayerSkill.Skills.size - 1;
            }
        }
        else
        {
            _selectedIndex++;
            if(_selectedIndex >= (int)PlayerSkill.Skills.size)
            {
                _selectedIndex = 0;
            }
        }

        PrintSkillInfo((PlayerSkill.Skills)_selectedIndex);
    }

    public void SelectSkill()
    {
        Manager.Player.Skill = (PlayerSkill.Skills)_selectedIndex;
    }
}
