using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyMenu/GameRecordInfo")]
[Serializable]
public class GameRecordInfo : ScriptableObject
{
    public int beingPassedLevel = 1;
    public int levelIndex = 1;
    public List<int> specialRewards = new List<int>();
    public List<int> maxScores = new List<int>();

    public void OnEnable()
    {
        for (int i = 0; i < maxScores.Count; i++)
        {
            if (PlayerPrefs.HasKey("maxScore" + (i + 1).ToString()))
                maxScores[i] = PlayerPrefs.GetInt("maxScore" + (i + 1).ToString());            
        }
    }

    public void OnDestroy()
    {
        Save();
    }

    public void Save()
    {
        for (int i = 0; i < maxScores.Count; i++)
        {
            PlayerPrefs.SetInt("maxScore" + (i + 1).ToString(), maxScores[i]);
        }
        PlayerPrefs.Save();
    }

    public void Clear()
    {
        for (int i = 0; i < maxScores.Count; i++)
        {
            maxScores[i] = 0;

            PlayerPrefs.SetInt("maxScore" + (i + 1).ToString(), maxScores[i]);
        }
        PlayerPrefs.Save();
    }
}
