using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl : MonoBehaviour
{
    public GameRecordInfo gameRecordInfo;
    public float volume = 1;

    public int score = 0;
    public int maxScore = 0;
    public int specialRewards = 0;

    public int loseHearts = 0;

    [HideInInspector]
    public Model model;

    [HideInInspector]
    public View view;

    [HideInInspector]
    public GameManager gameManager;

    [HideInInspector]
    public AudioManager audioManager;

    [HideInInspector]
    public UIManager uiRoot;

    private void Awake()
    {
        model = GameObject.FindGameObjectWithTag("Model").GetComponent<Model>();
        view = GameObject.FindGameObjectWithTag("View").GetComponent<View>();

        audioManager = GetComponent<AudioManager>();
        gameManager = GetComponent<GameManager>();
        audioManager = GetComponent<AudioManager>();
        
    }

    private void Start()
    {
        uiRoot = GetComponent<InitializeUI>().uiRoot;
    }

    private void Update()
    {
        loseHearts = gameManager.player.playerInfo.maxHP - gameManager.player.HP;
    }
}
