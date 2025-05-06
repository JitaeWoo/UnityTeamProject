using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupOnDead : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPopup; // 게임 오버 팝업 오브젝트
    public Transform popuptransform;
    public GameObject blocker;
    private string canvas = "UI_Canvas";

    private void Start()
    {
        // PlayerManager의 OnDied 이벤트 구독
        Manager.Player.OnDied += ShowGameOverPopup;
    }

    // 게임 오버 팝업을 띄우는 메서드
    private void ShowGameOverPopup()
    {
        Time.timeScale = 0f;
        GameObject spawnLocation = GameObject.Find(canvas);
        Instantiate(blocker, spawnLocation.transform); // UI 씬 기준으로 블로커 생성
        // 게임 오버 팝업 활성화
        Instantiate(gameOverPopup, popuptransform);
    }
}
