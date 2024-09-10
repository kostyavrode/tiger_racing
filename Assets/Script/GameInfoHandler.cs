using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInfoHandler : MonoBehaviour
{
    private int money;
    private int score;
    private int bestScore;
    private bool isNeedToCount;
    private void Awake()
    {
        GameManager.onGameStateChange += CheckGameState;
        CheckInfo();
    }
    private void OnDisable()
    {
        GameManager.onGameStateChange -= CheckGameState;
    }
    private void FixedUpdate()
    {
        if (isNeedToCount)
        {
            //score++;
        }
    }
    private void CheckInfo()
    {
        if (!PlayerPrefs.HasKey("Money"))
        {
            PlayerPrefs.SetInt("Money", 0);
            money= 0;
            bestScore= 0;
            score = 0;
        }
        else
        {
            money = PlayerPrefs.GetInt("Money");
            bestScore = PlayerPrefs.GetInt("BestScore");
            score = 0;
        }
    }
    public void AddMoney()
    {
        money++;
    }
    public void MinusMoney(int count)
    {
        if (money >= count)
        {
            money -= count;
        }
    }
    public int GetMoney()
    {
        money = PlayerPrefs.GetInt("Money");
        return money;
    }
    public int GetScore()
    {
    return score; 
    }
    public int GetBestScore()
    {
        if (PlayerPrefs.HasKey("BestScore"))
        {
            bestScore = PlayerPrefs.GetInt("BestScore");
            if (bestScore<score)
            {
                bestScore = score;
                PlayerPrefs.SetInt("BestScore", bestScore);
            }
        }
        else
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", score);
        }
        return bestScore;
    }

    private void CheckGameState(GameState state)
    {
        switch(state)
        {
            case GameState.OFF:
                isNeedToCount = false;
                break;
            case GameState.PLAYING:
                isNeedToCount= true;
                break;
            case GameState.PAUSED:
                isNeedToCount = false;
                break;
            case GameState.FINISHED:
                isNeedToCount= false;
                break;
            case GameState.END:
                break;
        }
    }
}
