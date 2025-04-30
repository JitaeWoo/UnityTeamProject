using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupSpawner : MonoBehaviour
{
    public GameObject popupPrefab;  // �˾� ������
    private GameObject currentPopup;  // ���� Ȱ��ȭ�� �˾�

    // �˾��� �����ϴ� �޼���
    public void SpawnPopup()
    {
        // �˾� ����
        currentPopup = Instantiate(popupPrefab, transform.position, Quaternion.identity);

        // �˾��� X ��ư�� ã�Ƽ� Ŭ�� �� �˾��� �����ϵ��� ����
        Button closeButton = currentPopup.transform.Find("CloseButton_Title").GetComponent<Button>();
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(ClosePopup);
        }
    }

    // �˾��� �����ϴ� �޼���
    public void ClosePopup()
    {
        if (currentPopup != null)
        {
            Destroy(currentPopup);  // �˾��� ����
        }
    }
}

