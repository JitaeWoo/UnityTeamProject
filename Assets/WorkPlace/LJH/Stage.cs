using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private int monsterCount;
    [SerializeField] private float spawnMonster;
    ////OnStageStart �������� ����

    ////���̺� ����
    
    ////���� ��ü���� 0�϶� Ŭ����
    //while(monsterNum != 0)
    //{
    //    //IsWaveClear = true;
    //    //���̺�
    //    if (!finalWave)
    //    {
    //        naxtWave();
    //    }
    //    else
    //    {
    //        //OnStageClear �������� Ŭ����

    //        //Ŭ���� UIȭ�� ȣ��
    //        StageClaerUI();
    //        //ĳ���� ��ȭ ����

    //        //���� ������ ����������
    //        if (finalStage)
    //        {
    //            gameOver();
    //        }
    //        //������ ���������� �ƴ϶��
    //        else
    //        {
    //            //Ŭ����� ���� ���������� �̵�
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
