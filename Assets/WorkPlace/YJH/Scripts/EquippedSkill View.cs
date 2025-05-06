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
        // Manager.Player.Skill을 통해 현재 장착된 스킬을 가져옴
        PlayerSkill.Skills currentSkill = Manager.Player.Skill;

        // 스킬 정보를 UI에 반영
        UpdateSkillInfo(currentSkill);
    }

    private void UpdateSkillInfo(PlayerSkill.Skills skill)
    {
        switch (skill)
        {
            case PlayerSkill.Skills.GrenadeThrow:
                skillNameText.text = "폭발";
                skillInfoText.text = "일정 원 반경에 폭발을 일으켜 데미지를 준다.";
                break;
            case PlayerSkill.Skills.BeamShot:
                skillNameText.text = "빔";
                skillInfoText.text = "전방 직선 상에 데미지를 준다.";
                break;
            case PlayerSkill.Skills.Heal:
                skillNameText.text = "치유";
                skillInfoText.text = "플레이어의 체력을 일정 부분 회복한다.";
                break;
            case PlayerSkill.Skills.ClearBullet:
                skillNameText.text = "탄막 제거";
                skillInfoText.text = "전방 일정 부분의 적 투사체를 제거한다.";
                break;
            case PlayerSkill.Skills.Slash:
                skillNameText.text = "베기";
                skillInfoText.text = "전방 부채꼴 범위의 적을 베어 데미지를 준다.";
                break;
        }
    }
}