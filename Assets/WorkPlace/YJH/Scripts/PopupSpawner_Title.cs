using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupSpawner : MonoBehaviour
{
    public GameObject popupPrefab;  // 팝업 프리팹
    private GameObject currentPopup;  // 현재 활성화된 팝업

    // 팝업을 생성하는 메서드
    public void SpawnPopup()
    {
        // 팝업 생성
        currentPopup = Instantiate(popupPrefab, transform.position, Quaternion.identity);

        // 팝업의 X 버튼을 찾아서 클릭 시 팝업을 삭제하도록 설정
        Button closeButton = currentPopup.transform.Find("CloseButton_Title").GetComponent<Button>();
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(ClosePopup);
        }
    }

    // 팝업을 삭제하는 메서드
    public void ClosePopup()
    {
        if (currentPopup != null)
        {
            Destroy(currentPopup);  // 팝업을 삭제
        }
    }
}

