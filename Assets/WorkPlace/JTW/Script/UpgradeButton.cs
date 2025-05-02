using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _statInfoPrefab;
    private StatUpgrader _statUpgrader = new StatUpgrader();
    private List<int> _statList;
    private float _textY = 150;
    private int _upgradeStatsCount = 3;

    private void Start()
    {
        _statList = Util.GetRandomNums(_upgradeStatsCount, (int)StatUpgrader.Stats.Size);
        foreach (int num in _statList)
        {
            TextMeshProUGUI textUI = Instantiate(_statInfoPrefab, transform);
            textUI.transform.localPosition = new Vector3(0, _textY, 0);
            textUI.text = _statUpgrader.GetStatUpgradeInfo((StatUpgrader.Stats)num);
            _textY -= 100;
        }
    }

    public void UpgradeStats()
    {
        foreach(int num in _statList)
        {
            Debug.Log((StatUpgrader.Stats)num);
            _statUpgrader.UpgradeStat((StatUpgrader.Stats)num);
        }
    }
}
