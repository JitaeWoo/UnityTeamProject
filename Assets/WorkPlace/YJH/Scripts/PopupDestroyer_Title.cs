using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupCloseButton : MonoBehaviour
{
    // X ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void ClosePopup()
    {
        // �ڽ��� �θ� ������Ʈ(�˾�â)�� �ı�
        Destroy(transform.parent.gameObject);
    }
}
