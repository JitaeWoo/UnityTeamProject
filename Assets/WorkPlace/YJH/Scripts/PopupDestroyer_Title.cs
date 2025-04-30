using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupCloseButton : MonoBehaviour
{
    // X 버튼 클릭 시 호출되는 함수
    public void ClosePopup()
    {
        // 자신의 부모 오브젝트(팝업창)를 파괴
        Destroy(transform.parent.gameObject);
    }
}
