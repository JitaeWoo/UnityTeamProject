using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupOnDead : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPopup; // 게임 오버 팝업 오브젝트
    public Transform popuptransform;

    private void Start()
    {
        // PlayerManager의 OnDied 이벤트 구독
        Manager.Player.OnDied += ShowGameOverPopup;
    }

    // 게임 오버 팝업을 띄우는 메서드
    private void ShowGameOverPopup()
    {
        // 게임 오버 팝업 활성화
        Instantiate(gameOverPopup, popuptransform);
    }
}
