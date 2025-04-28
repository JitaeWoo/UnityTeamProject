using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // ���� �� ���� �� UI ���� Additive ������� �ε�
    void Start()
    {
        LoadUI();
    }

    void LoadUI()
    {
        SceneManager.LoadScene("UI_Scene", LoadSceneMode.Additive);
    }

    // ���� ���� �� UI ���� ��Ȱ��ȭ
    public void UnloadUI()
    {
        // UI ���� ��ε��Ͽ� ���� �� ������ UI�� ����
        SceneManager.UnloadSceneAsync("UI_Scene");
    }
}