using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum GameState
{
    OFF = 0,
    PLAYING = 1,
    PAUSED = 2,
    FINISHED = 3,
    END=4
}
public class GameManager : MonoBehaviour
{
    public static Action<GameState> onGameStateChange;
    [SerializeField]private GameState state;
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    public void StartGame()
    {
        ChangeGameState(GameState.PLAYING);
    }
    public void PauseGame() 
    { 
        ChangeGameState(GameState.PAUSED); 
    }
    public void FinishGame()
    {
        ChangeGameState(GameState.FINISHED);
    }
    public void EndGame()
    {
        ChangeGameState(GameState.END);
    }
    private void ChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.OFF:
                break;
            case GameState.PLAYING:
                break;
            case GameState.PAUSED:
                break;
            case GameState.FINISHED:
                break;
            case GameState.END:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
        }
        this.state = state;
        onGameStateChange?.Invoke(state);
    }
}

