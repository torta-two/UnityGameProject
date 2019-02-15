using UnityEngine;
using System;

public class Model : MonoBehaviour
{
    public event Action<int> OnScoreChange;
    public event Action<int> OnMaxScoreChange;
    public event Action<int, int> OnBalance;

    private int score = 0;
    private int maxScore = 0;
    private int specialCoin = 0;

    private void Start()
    {
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
        //玩的是玩过的，有记录的关卡
        if (GameRecord.Instance.currentLevelIndex < GameRecord.Instance.beingPassedLevel)
        {
            maxScore = GameRecord.Instance.maxScore[GameRecord.Instance.currentLevelIndex - 1];
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
                    GameRecord.Instance.getCoin++;
                }
                break;
            case "CommonEnemy":
                {
                    score += 10;
                    GameRecord.Instance.killMonster++;
                }
                break;
            case "SpecialEnemy":
                {
                    score += 100;
                    GameRecord.Instance.killMonster++;
                }
                break;
            default: break;
        }
    }

    public void SaveScore()
    {
        OnBalance?.Invoke(score, specialCoin);

        if (GameRecord.Instance.maxScore[GameRecord.Instance.currentLevelIndex - 1] < maxScore)
            GameRecord.Instance.maxScore[GameRecord.Instance.currentLevelIndex - 1] = maxScore;

        if (GameRecord.Instance.specialCoin[GameRecord.Instance.currentLevelIndex - 1] < specialCoin)
            GameRecord.Instance.specialCoin[GameRecord.Instance.currentLevelIndex - 1] = specialCoin;

        if (GameRecord.Instance.currentLevelIndex == GameRecord.Instance.beingPassedLevel)
            GameRecord.Instance.beingPassedLevel++;
       
        GameRecord.Instance.currentLevelIndex++;

        GameRecord.Instance.Save(GameRoot.GameRecordJsonSavePath);
    }
}
