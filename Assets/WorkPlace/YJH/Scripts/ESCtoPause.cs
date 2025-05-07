using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscapeKeyHandler : MonoBehaviour
{
    [SerializeField] private Button targetButton;

    private void OnEnable()
    {
        Manager.Game.OnPaused += OnPaused;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && targetButton.interactable)
        {
            targetButton.onClick.Invoke(); // ��ư ������ ����
        }
    }

    private void OnDisable()
    {
        Manager.Game.OnPaused -= OnPaused;
    }

    private void OnPaused(bool isPause)
    {
        targetButton.interactable = !isPause;
    }
}
