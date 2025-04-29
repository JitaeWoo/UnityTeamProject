using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public enum Skills
    {
        Dash, ClearBullet
    }

    [SerializeField] private Skills _selectedSkill;
    private Skill _skill;

    private void Awake()
    {
        EquipSkill(_selectedSkill);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _skill.Use();
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
            case Skills.Dash:
                _skill = gameObject.AddComponent<Dash>();
                break;
            case Skills.ClearBullet:
                _skill = gameObject.AddComponent<ClearBullet>();
                break;
        }
    }
}
