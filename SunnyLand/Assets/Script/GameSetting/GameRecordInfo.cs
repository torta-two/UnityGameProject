using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

[CreateAssetMenu(menuName = "MyMenu/GameRecordInfo")]
[Serializable]
public class GameRecordInfo : ScriptableObject
{
    public int beingPassedLevel = 1;
    public int levelIndex = 1;
    public int money = 0;
    public int killMonster = 0;
    public int GetCoin = 0;

    public List<int> specialCoin = new List<int>();
    public List<int> maxScore = new List<int>();

    public List<PlayerControl> playerPrefabList = new List<PlayerControl>();

    [HideInInspector]
    public List<int> taskState = new List<int>(5);

    public void Load()
    {
        LoadData(ref beingPassedLevel);
        LoadData(ref levelIndex);
        LoadData(ref money);
        LoadData(ref killMonster);
        LoadData(ref GetCoin);
        LoadListData(ref specialCoin, "specialCoin");
        LoadListData(ref maxScore, "maxScore");
        LoadListData(ref taskState, "taskState");
    }

    public void Save()
    {
        PlayerPrefs.SetInt("beingPassedLevel", beingPassedLevel);
        PlayerPrefs.SetInt("levelIndex", levelIndex);
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.SetInt("killMonster", killMonster);
        PlayerPrefs.SetInt("getCoin", GetCoin);
        SaveListData(specialCoin, "specialCoin");
        SaveListData(maxScore, "maxScore");
        SaveListData(taskState, "taskState");

        PlayerPrefs.Save();
    }


    private void LoadData(ref int data)
    {
        if (PlayerPrefs.HasKey(data.ToString()))
        {
            data = PlayerPrefs.GetInt(data.ToString());
        }
    }

    private void LoadListData(ref List<int> list, string loadName)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (PlayerPrefs.HasKey(list.ToString() + (i + 1).ToString()))
            {
                list[i] = PlayerPrefs.GetInt(loadName + (i + 1).ToString());
            }
        }
    }


    private void SaveListData(List<int> list, string saveName)
    {
        for (int i = 0; i < list.Count; i++)
        {
            PlayerPrefs.SetInt(saveName + (i + 1).ToString(), list[i]);
        }
    }



    //public void Clear()
    //{
    //    for (int i = 0; i < maxScores.Count; i++)
    //    {
    //        maxScores[i] = 0;
    //        specialCoins[i] = 0;

    //        PlayerPrefs.SetInt("maxScore" + (i + 1).ToString(), maxScores[i]);
    //        PlayerPrefs.SetInt("specialCoin" + (i + 1).ToString(), specialCoins[i]);
    //    }
    //    PlayerPrefs.Save();
    //}
}
