using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EquippedSkill : MonoBehaviour
{
    private PlayerSkill skillComponent;  // 필드 초기화 제거
    public TMP_Text skillNameText;

    private void Awake()
    {
        // Awake에서 skillComponent 초기화
        skillComponent = Manager.Player.GetComponent<PlayerSkill>();

        // 장착된 스킬의 이름을 가져와서 텍스트로 표시
        DisplaySkillName();
    }

    public void DisplaySkillName()
    {
        // 장착된 스킬의 이름을 가져옴
        string displayName = GetSkillDisplayName(skillComponent.EquippedSkillType);

        // 텍스트 UI에 표시
        skillNameText.text = displayName;
    }

    string GetSkillDisplayName(PlayerSkill.Skills skill)
    {
        switch (skill)
        {
            case PlayerSkill.Skills.GrenadeThrow: return "폭발";
            case PlayerSkill.Skills.BeamShot: return "빔";
            case PlayerSkill.Skills.Heal: return "치유";
            case PlayerSkill.Skills.ClearBullet: return "탄막 제거";
            case PlayerSkill.Skills.Slash: return "베기";
            default: return "알 수 없음";
        }
    }
}
