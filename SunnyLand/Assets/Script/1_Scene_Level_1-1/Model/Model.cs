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

    public void GetScore(string tag)
    {
        switch (tag)
        {
            case "CommonReward":
                {
                    ctrl.score += 10;
                }
                break;
            case "SpecialReward": ctrl.score += 100; break;
            case "Enemy": ctrl.score += 50; break;
            default: break;
        }
    }


}
