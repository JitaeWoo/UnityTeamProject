using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Portrait_SetImage : MonoBehaviour
{
    public Image targetImage;

    void Start()
    {
        if (!string.IsNullOrEmpty(SceneData.spriteToSet))
        {
            Sprite sprite = Resources.Load<Sprite>(SceneData.spriteToSet);
            if (sprite != null)
            {
                targetImage.sprite = sprite;
            }
            else
            {
                Debug.LogWarning("��������Ʈ�� ã�� �� �����ϴ�: " + SceneData.spriteToSet);
            }

            // ���� �� �ʱ�ȭ
            SceneData.spriteToSet = null;
        }
    }
}
