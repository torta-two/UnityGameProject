using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "MyMenu/GameRecordInfo")]
[Serializable]
public class GameRecordInfo : ScriptableObject
{
    public int beingPassedLevel = 1;
    public int levelIndex = 1;
    public int money = 0;
    public int killMonster = 0;
    public int getCoin = 0;

    public List<int> specialCoin = new List<int>();
    public List<int> maxScore = new List<int>();

    [HideInInspector]
    public List<PlayerControl> playerPrefabList = new List<PlayerControl>();

    public List<int> taskState = new List<int>(5);



    private static GameRecordInfo _instance;
    public static GameRecordInfo Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.FindObjectsOfTypeAll<GameRecordInfo>().FirstOrDefault();
            }
            return _instance;
        }
    }


    public static void LoadFromJSON(string path)
    {
        if (_instance != null) DestroyImmediate(_instance);
        _instance = CreateInstance<GameRecordInfo>();
        JsonUtility.FromJsonOverwrite(System.IO.File.ReadAllText(path), _instance);
        _instance.hideFlags = HideFlags.HideAndDontSave;
    }

    public static void InitializeFromDefault(GameRecordInfo gameRecord)
    {
        if (_instance != null) DestroyImmediate(_instance);
        _instance = Instantiate(gameRecord);
        _instance.hideFlags = HideFlags.HideAndDontSave;
    }

    public void SaveToJSON(string path)
    {
        Debug.LogFormat("Saving game settings to {0}", path);
        if(!System.IO.File.Exists(path))
            System.IO.File.WriteAllText(path, "");

        System.IO.File.WriteAllText(path, JsonUtility.ToJson(this, true));
    }

    




    public void Load()
    {
        LoadData(ref beingPassedLevel, "0_beingPassedLevel");
        LoadData(ref levelIndex, "0_levelIndex");
        LoadData(ref money, "0_money");
        LoadData(ref killMonster, "0_killMonster");
        LoadData(ref getCoin, "0_getCoin");
        LoadListData(ref specialCoin, "specialCoin");
        LoadListData(ref maxScore, "maxScore");
        LoadListData(ref taskState, "taskState");

        if (beingPassedLevel == levelIndex)
        {
            //在玩最新一关，如果最高分list和特殊硬币list容量不够，扩容一个
            if (maxScore.Count < levelIndex)
                maxScore.Add(0);
            if (specialCoin.Count < levelIndex)
                specialCoin.Add(0);
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt("0_beingPassedLevel", beingPassedLevel);
        PlayerPrefs.SetInt("0_levelIndex", levelIndex);
        PlayerPrefs.SetInt("0_money", money);
        PlayerPrefs.SetInt("0_killMonster", killMonster);
        PlayerPrefs.SetInt("0_getCoin", getCoin);
        SaveListData(specialCoin, "specialCoin");
        SaveListData(maxScore, "maxScore");
        SaveListData(taskState, "taskState");

        PlayerPrefs.Save();
    }

    private void OnEnable()
    {
        //Load();

        if (playerPrefabList.Count == 0)
        {
            GameObject player_Fox = Resources.Load("Prefab/Player/Player_Fox") as GameObject;
            GameObject player_Rabbit = Resources.Load("Prefab/Player/Player_Rabbit") as GameObject;
            playerPrefabList.Add(player_Fox.GetComponent<PlayerControl>());
            playerPrefabList.Add(player_Rabbit.GetComponent<PlayerControl>());
        }
    }





    private void LoadData(ref int data, string dataName)
    {
        if (PlayerPrefs.HasKey(dataName))
        {
            data = PlayerPrefs.GetInt(dataName);
        }
    }

    private void LoadListData(ref List<int> list, string loadName)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (PlayerPrefs.HasKey(list.ToString() + (i + 1).ToString()))
            {
                list[i] = PlayerPrefs.GetInt((i + 1).ToString() + "_" + loadName);
            }
        }
    }


    private void SaveListData(List<int> list, string saveName)
    {
        for (int i = 0; i < list.Count; i++)
        {
            PlayerPrefs.SetInt((i + 1).ToString() + "_" + saveName, list[i]);
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
