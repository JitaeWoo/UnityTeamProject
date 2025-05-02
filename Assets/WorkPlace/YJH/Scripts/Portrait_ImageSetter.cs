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
                Debug.LogWarning("스프라이트를 찾을 수 없습니다: " + SceneData.spriteToSet);
            }

            // 적용 후 초기화
            SceneData.spriteToSet = null;
        }
    }
}
