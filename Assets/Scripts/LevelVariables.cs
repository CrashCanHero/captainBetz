using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Net;
using UnityEngine.UI;

public class LevelVariables : MonoBehaviour
{
    public static GameObject self;
    public static float AttackPower = 1, Speed = 10, Defense = 1;
    public static int Round = 1;
    public int round = 0;

    public static int NumOfEnemies;
    public static int Deaths;
    public static int EnemiesKilled;
    public static int Score;
    public static bool runninground = true;

    public static int timer = 80;


    public void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (self == null)
        {
            self = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        Score = EnemiesKilled * 10;
        if (NumOfEnemies <= 0)
        {
            LevelVariables.runninground = false;
            EndRound();
        }

        if (CharacterScript.HP > 0)
        {
            if (Input.GetKeyUp(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void EndRound()
    {
        if (timer > 0)
        {
            timer--;
        }
    }
}
