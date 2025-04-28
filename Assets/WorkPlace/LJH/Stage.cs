using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private int monsterCount;
    [SerializeField] private float spawnMonster;
    ////OnStageStart 스테이지 시작

    ////웨이브 시작
    
    ////몬스터 개체수가 0일때 클리어
    //while(monsterNum != 0)
    //{
    //    //IsWaveClear = true;
    //    //웨이브
    //    if (!finalWave)
    //    {
    //        naxtWave();
    //    }
    //    else
    //    {
    //        //OnStageClear 스테이지 클리어

    //        //클리어 UI화면 호출
    //        StageClaerUI();
    //        //캐릭터 강화 선택

    //        //만일 마지막 스테이지면
    //        if (finalStage)
    //        {
    //            gameOver();
    //        }
    //        //마지막 스테이지가 아니라면
    //        else
    //        {
    //            //클리어시 다음 스테이지로 이동
    //            nextStage();
    //        }
    //    }
    //}
    public void Awake()
    {
        monsterCount = 1;
        spawnMonster = 3;
        
    }
    
}
