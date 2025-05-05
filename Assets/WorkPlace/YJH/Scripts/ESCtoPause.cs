using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscapeKeyHandler : MonoBehaviour
{
    [SerializeField] private Button targetButton;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            targetButton.onClick.Invoke(); // ��ư ������ ����
        }
    }
}
