using UnityEngine;
using System;

public class Model : MonoBehaviour
{
    [HideInInspector]
    public GameRecordInfo gameRecord;
    public event Action<int> OnScoreChange;
    public event Action<int> OnMaxScoreChange;
    public event Action<int, int> OnBalance;

    private int score = 0;
    private int maxScore = 0;
    private int specialCoin = 0;

    private void Awake()
    {
        gameRecord = FindObjectOfType<GameRoot>().gameRecord;
        LoadScore();
    }

    private void Update()
    {
        if (maxScore < score)
            maxScore = score;

        OnMaxScoreChange?.Invoke(maxScore);

        OnScoreChange?.Invoke(score);
    }

    public void LoadScore()
    {
        gameRecord.Load();

        //正在玩最新一关，没有记录
        if (gameRecord.levelIndex == gameRecord.beingPassedLevel)
        {
            if (gameRecord.maxScore.Count < gameRecord.levelIndex)
                gameRecord.maxScore.Add(maxScore);
            if (gameRecord.specialCoin.Count < gameRecord.levelIndex)
                gameRecord.specialCoin.Add(specialCoin);
        }
        else
        {
            maxScore = gameRecord.maxScore[gameRecord.levelIndex - 1];
        }
    }

    public void GetScore(string tag)
    {
        switch (tag)
        {
            case "CommonCoin":
                {
                    score += 10;
                }
                break;
            case "SpecialCoin":
                {
                    score += 50;
                    specialCoin++;
                    gameRecord.GetCoin++;
                }
                break;
            case "CommonEnemy":
                {
                    score += 10;
                    gameRecord.killMonster++;
                }
                break;
            case "SpecialEnemy":
                {
                    score += 100;
                    gameRecord.killMonster++;
                }
                break;
            default: break;
        }
    }

    public void SaveScore()
    {
        OnBalance?.Invoke(score, specialCoin);

        if (gameRecord.maxScore[gameRecord.levelIndex - 1] < maxScore)
        gameRecord.maxScore[gameRecord.levelIndex - 1] = maxScore;

        if (gameRecord.specialCoin[gameRecord.levelIndex - 1] < specialCoin)
            gameRecord.specialCoin[gameRecord.levelIndex - 1] = specialCoin;

        if(gameRecord.levelIndex == gameRecord.beingPassedLevel)
            gameRecord.beingPassedLevel++;

        gameRecord.Save();
    }   
}
