using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour {

    int score = 0;
    [SerializeField] private int lives = 3;
    [SerializeField] int bossScore = 10000;
    
    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
        if (score > bossScore)
        {
            FindObjectOfType<EnemySpawner>().BossIncoming();
        }
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
    
    public int GetLives()
    {
        return lives;
    }

    public void RemoveLive()
    {
        lives--;
    }

}