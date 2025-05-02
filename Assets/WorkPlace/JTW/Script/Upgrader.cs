using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrader : MonoBehaviour
{
    
    [SerializeField] private GameObject _upgradeButtonPrefab;
    private float _buttonX = -500;

    void Start()
    {
        CreateButton();
    }

    private void CreateButton()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject button = Instantiate(_upgradeButtonPrefab, transform);
            button.transform.localPosition = new Vector3(_buttonX, 0, 0);
            _buttonX += 500;
        }
    }
}
