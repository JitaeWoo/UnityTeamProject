using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState
{
    Title,             // Ÿ��Ʋ ȭ��
    Controls,          // ���۹� ȭ��
    InGame,            // ���� ȭ��
    Pause,             // �Ͻ����� ȭ��
    PlayerInfo,        // �÷��̾� ���� ȭ��
    GameOver,          // ���� ���� �˸�
    StageStart,        // �������� ���� �˸�
    WaveStart,         // ���̺� ���� �˸�
    MiddleBossEncounter,// �߰� ���� ��ī���� �˸�
    FinalBossEncounter,// ���� ���� ��ī���� �˸�
    RewardChoice,      // ���� ������ �˾�
}

public class UIManager : Singleton<UIManager>
{
    public static UIManager Instance { get; private set; }

    // ������ â���� �ش� UI �� �������� ��
    [Header("UI Panels")]
    public GameObject titleUI;
    public GameObject controlsUI;
    public GameObject inGameHUD;
    public GameObject pauseMenu;
    public GameObject playerInfoUI;
    public GameObject gameOverUI;
    public GameObject stageStartUI;
    public GameObject waveStartUI;
    public GameObject middleBossEncounterUI;
    public GameObject finalBossEncounterUI;
    public GameObject rewardChoiceUI;

    private UIState currentState; // ���� UI ǥ��

    protected void Awake()
    {
        UIManager.CreateInstance();  // Singleton �ν��Ͻ� ����
        DontDestroyOnLoad(gameObject);  // �� ��ȯ �� �ı����� �ʵ��� ����
    }

    public void ShowTitleUI()
    {
        ChangeState(UIState.Title);
        ToggleAllUI(false);
        titleUI.SetActive(true);
    }

    public void ShowControlsUI()
    {
        ChangeState(UIState.Controls);
        ToggleAllUI(false);
        controlsUI.SetActive(true);
    }

    public void ShowInGameHUD()
    {
        ChangeState(UIState.InGame);
        ToggleAllUI(false);
        inGameHUD.SetActive(true);
    }

    public void ShowPauseMenu()
    {
        ChangeState(UIState.Pause);
        ToggleAllUI(false);
        pauseMenu.SetActive(true);
    }

    public void ShowPlayerInfoUI()
    {
        ChangeState(UIState.PlayerInfo);
        ToggleAllUI(false);
        playerInfoUI.SetActive(true);
    }

    public void ShowGameOverUI()
    {
        ChangeState(UIState.GameOver);
        ToggleAllUI(false);
        gameOverUI.SetActive(true);
    }

    public void ShowStageStartUI()
    {
        ChangeState(UIState.StageStart);
        ToggleAllUI(false);
        stageStartUI.SetActive(true);
    }

    public void ShowWaveStartUI()
    {
        ChangeState(UIState.WaveStart);
        ToggleAllUI(false);
        waveStartUI.SetActive(true);
    }

    public void ShowMiddleBossEncounterUI()
    {
        ChangeState(UIState.FinalBossEncounter);
        ToggleAllUI(false);
        finalBossEncounterUI.SetActive(true);
    }

    public void ShowFinalBossEncounterUI()
    {
        ChangeState(UIState.MiddleBossEncounter);
        ToggleAllUI(false);
        middleBossEncounterUI.SetActive(true);
    }

    public void ShowRewardChoiceUI()
    {
        ChangeState(UIState.RewardChoice);
        ToggleAllUI(false);
        rewardChoiceUI.SetActive(true);
    }

    public void ResumeGame()
    {
        ShowInGameHUD();
        Time.timeScale = 1f; // ���� �簳
    }

    public void QuitGame()
    {
        // ���� ���� ó�� (���� �޴��� ���ư��� ��)
        ShowTitleUI();
        Time.timeScale = 1f;
    }

    private void ChangeState(UIState newState)
    {
        currentState = newState;
        Debug.Log($"UI State changed to: {currentState}"); // Ȯ�ο� �α� ���
    }

    // ��ü UI Ȱ��ȭ ���� ���� ����
    private void ToggleAllUI(bool isActive)
    {
        titleUI.SetActive(isActive);
        controlsUI.SetActive(isActive);
        inGameHUD.SetActive(isActive);
        pauseMenu.SetActive(isActive);
        playerInfoUI.SetActive(isActive);
        gameOverUI.SetActive(isActive);
        stageStartUI.SetActive(isActive);
        waveStartUI.SetActive(isActive);
        finalBossEncounterUI.SetActive(isActive);
        rewardChoiceUI.SetActive(isActive);
    }
}