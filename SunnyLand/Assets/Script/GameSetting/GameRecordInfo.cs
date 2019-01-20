using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyMenu/GameRecordInfo")]
[Serializable]
public class GameRecordInfo : ScriptableObject
{
    public int beingPassedLevel = 1;
    public int thisLevel = 1;
    public List<int> levelSpecialReward = new List<int>();
    public List<int> levelMaxScore = new List<int>();

    public void OnEnable()
    {
        for (int i = 0; i < levelMaxScore.Count; i++)
        {
            if (PlayerPrefs.HasKey("levelMaxScore" + (i + 1).ToString()))
                levelMaxScore[i] = PlayerPrefs.GetInt("levelMaxScore" + (i + 1).ToString());            
        }
    }

    public void OnDestroy()
    {
        Save();
    }

    public void Save()
    {
        for (int i = 0; i < levelMaxScore.Count; i++)
        {
            PlayerPrefs.SetInt("levelMaxScore" + (i + 1).ToString(), levelMaxScore[i]);
        }
        PlayerPrefs.Save();
    }

    public void Clear()
    {
        for (int i = 0; i < levelMaxScore.Count; i++)
        {
            levelMaxScore[i] = 0;

            PlayerPrefs.SetInt("levelMaxScore" + (i + 1).ToString(), levelMaxScore[i]);
        }
        PlayerPrefs.Save();
    }
}
