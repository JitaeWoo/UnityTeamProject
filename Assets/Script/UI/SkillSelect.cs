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
                _skillName.text = "폭발";
                _skillInfo.text = "일정 원 반경에 폭발을 일으켜 데미지를 준다.";
                break;
            case PlayerSkill.Skills.BeamShot:
                _skillName.text = "빔";
                _skillInfo.text = "전방 직선 상에 데미지를 준다.";
                break;
            case PlayerSkill.Skills.Heal:
                _skillName.text = "치유";
                _skillInfo.text = "플레이어의 체력을 일정 부분 회복한다.";
                break;
            case PlayerSkill.Skills.ClearBullet:
                _skillName.text = "탄막 제거";
                _skillInfo.text = "전방 일정 부분의 적 투사체를 제거한다.";
                break;
            case PlayerSkill.Skills.Slash:
                _skillName.text = "베기";
                _skillInfo.text = "전방 부채꼴 범위의 적을 베어 데미지를 준다.";
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
