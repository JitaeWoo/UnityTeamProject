using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState
{
    Title,             // 타이틀 화면
    Controls,          // 조작법 화면
    InGame,            // 게임 화면
    Pause,             // 일시정지 화면
    PlayerInfo,        // 플레이어 정보 화면
    GameOver,          // 게임 오버 알림
    StageStart,        // 스테이지 진입 알림
    WaveStart,         // 웨이브 진입 알림
    MiddleBossEncounter,// 중간 보스 인카운터 알림
    FinalBossEncounter,// 최종 보스 인카운터 알림
    RewardChoice,      // 보상 선택지 팝업
}

public class UIManager : Singleton<UIManager>
{
    public static UIManager Instance { get; private set; }

    // 에디터 창에서 해당 UI 를 연결해줄 것
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

    private UIState currentState; // 현재 UI 표시

    protected void Awake()
    {
        UIManager.CreateInstance();  // Singleton 인스턴스 생성
        DontDestroyOnLoad(gameObject);  // 씬 전환 시 파괴되지 않도록 설정
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
        Time.timeScale = 1f; // 게임 재개
    }

    public void QuitGame()
    {
        // 게임 종료 처리 (메인 메뉴로 돌아가기 등)
        ShowTitleUI();
        Time.timeScale = 1f;
    }

    private void ChangeState(UIState newState)
    {
        currentState = newState;
        Debug.Log($"UI State changed to: {currentState}"); // 확인용 로그 출력
    }

    // 전체 UI 활성화 여부 조절 목적
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