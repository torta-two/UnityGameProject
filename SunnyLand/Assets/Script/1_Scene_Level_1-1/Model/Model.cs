using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{
    private Ctrl ctrl;
    private int thisLevel = 0;

    private void Awake()
    {
        ctrl = GameObject.FindWithTag("Ctrl").GetComponent<Ctrl>();

        thisLevel = ctrl.gameRecordInfo.thisLevel;
    }

    private void Update()
    {
        if (thisLevel < ctrl.gameRecordInfo.beingPassedLevel)
        {
            ctrl.maxScore = ctrl.gameRecordInfo.levelMaxScore[thisLevel - 1];
            if (ctrl.gameRecordInfo.levelMaxScore[thisLevel - 1] < ctrl.score)
            {
                ctrl.gameRecordInfo.levelMaxScore[thisLevel - 1] = ctrl.score;
            }
        }

        if (ctrl.maxScore < ctrl.score)
        {
            ctrl.maxScore = ctrl.score;
        }
    }

    public void OnEnemyDead(string enemyTag)
    {
        switch (tag)
        {
            case "CommonEnemy": ctrl.score += 50;break;
            case "SpecialEnemy": ctrl.score += 100; break;
            default: break;
        }
    }

    public void OnGetCoins(string coinTag)
    {
        switch (tag)
        {
            case "CommonCoin": ctrl.score += 20; break;
            case "SpecialCoin": ctrl.score += 100; break;
            default: break;
        }
    }

}
