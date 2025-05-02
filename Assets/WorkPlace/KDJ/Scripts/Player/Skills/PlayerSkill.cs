using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public enum Skills
    {
        GrenadeThrow, BeamShot, Heal, ClearBullet, Slash
    }

    [SerializeField] private Skills _selectedSkill;
    private Skill _skill;
    private Skill _utilitySkill;

    private void Awake()
    {
        EquipSkill(_selectedSkill);
        _utilitySkill = gameObject.AddComponent<Dash>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _skill.Use();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _utilitySkill.Use();
        }
    }

    public void EquipSkill(Skills skill)
    {
        if(_skill != null)
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
}
