using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EquippedSkill_Infoss : MonoBehaviour
{
    public TMP_Text skillInfo;

    public void PrintSkillInfo()
    {
        skillInfo.text = $"{Manager.Player.Skill}";
    }
}
