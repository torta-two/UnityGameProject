using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "MyMenu/GameRecord")]
[Serializable]
public class GameRecord : ScriptableObject
{
    private static GameRecord _instance;
    public static GameRecord Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.FindObjectsOfTypeAll<GameRecord>().FirstOrDefault();
            }
            return _instance;
        }
    }

    public static string[] PlayerPrefabPathList { get; } =
    {
        "Prefab/Player/Player_Fox",
        "Prefab/Player/Player_Rabbit"
    };

    public int beingPassedLevel = 1;
    public int currentLevelIndex = 1;
    public int money = 0;
    public int killMonster = 0;
    public int getCoin = 0;

    public int[] specialCoin = new int[6];
    public int[] maxScore = new int[6];
    public int[] taskState = new int[5];

    public static void Load(string path, GameRecord gameRecord)
    {
        if (path == null) Debug.Log("GameRecordPath is null!");
        if (gameRecord == null) Debug.Log("GameRecord is null!");

        if (System.IO.File.Exists(path))
        {
            _instance = CreateInstance<GameRecord>();
            JsonUtility.FromJsonOverwrite(System.IO.File.ReadAllText(path), _instance);
        }
        else
        {
            _instance = Instantiate(gameRecord);
            System.IO.File.WriteAllText(path, "");
        }
        _instance.hideFlags = HideFlags.HideAndDontSave;
        _instance.Save(path);
    }

    public void Save(string path)
    {
        System.IO.File.WriteAllText(path, JsonUtility.ToJson(this, true));
    }


#if UNITY_EDITOR
    [UnityEditor.MenuItem("Tools/Game Record")]
    public static void ShowGameRecord()
    {
        UnityEditor.Selection.activeObject = Instance;
    }
#endif


}
