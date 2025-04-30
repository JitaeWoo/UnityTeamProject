using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MonsterInitiation {
    public GameObject Monster;
    public Transform SpawnPoint;
}

public class TestStageManagerCYE : MonoBehaviour
{
    [SerializeField] private GameObject[] _monsterPrefabList;
    [SerializeField] private Transform[] _monsterSpawnpoint;
    [SerializeField] private Transform _playerSpawnPoint;
    private Stack<MonsterInitiation> SpawnSlot;


    // Start is called before the first frame update
    void OnEnable()
    {
        Manager.Player.CreatePlayer(_playerSpawnPoint.position);

        SpawnSlot = new Stack<MonsterInitiation>(_monsterSpawnpoint.Length);
        for (int cnt = 0; cnt < _monsterSpawnpoint.Length; cnt++) {
            MonsterInitiation monsterInitiation = new MonsterInitiation { Monster = _monsterPrefabList[cnt], SpawnPoint = _monsterSpawnpoint[cnt] };
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
