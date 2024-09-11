using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] uiPanels;
    [SerializeField] private TMP_Text[] moneyBar;
    public TMP_Text scoreBar;
    [SerializeField] private TMP_Text bestScoreBar;
    public TMP_Text timeBar;
    [SerializeField] public LevelManager[] levelManagers;
    [SerializeField] private GameObject[] roads;
    public bool isWin;
    private GameManager gameManager;
    private LevelManager currentLevel;
    private GameInfoHandler gameInfoHandler;
    private AudioManager audioManager;
    private void Start()
    {
        gameInfoHandler = ServiceLocator.GetService<GameInfoHandler>();
        gameManager= ServiceLocator.GetService<GameManager>();
        audioManager = ServiceLocator.GetService<AudioManager>();
        ShowMoney();
        GameManager.onGameStateChange += CheckGameState;
    }
    private void CheckGameState(GameState state)
    {
        switch (state)
        {
            case GameState.OFF:
                break;
            case GameState.PLAYING:
                ShowScore();
                break;
            case GameState.PAUSED:
                break;
            case GameState.FINISHED:
                bestScoreBar.text = gameInfoHandler.GetBestScore().ToString();
                if (!isWin)
                {
                    uiPanels[3].SetActive(true);
                    uiPanels[2].SetActive(false);
                }
                else
                {
                    uiPanels[2].SetActive(false );
                    uiPanels[4].SetActive(true );
                }
                break;
            case GameState.END:
                GameManager.onGameStateChange -= CheckGameState;
                break;
        }
    }
    public void StartLevel(int level)
    {
        currentLevel=Instantiate(levelManagers[level]);
        roads[levelManagers[level].levelNum-1].SetActive(true);
        currentLevel.levelNum = level;
    }
    public void ShowMoney()
    {
        foreach (TMP_Text t in moneyBar)
        {
            t.text = gameInfoHandler.GetMoney().ToString();
        }
        
    }
    public void ShowScore()
    {
        scoreBar.text= gameInfoHandler.GetScore().ToString();
    }
    public void ShowBestScore()
    {
        bestScoreBar.text=gameInfoHandler.GetBestScore().ToString();
    }
    public void StartButton()
    {
        ServiceLocator.GetService<GameManager>().StartGame();
        //StartLevel(0);
    }
    public void PauseButton()
    {
        ServiceLocator.GetService<GameManager>().PauseGame();
    }
    public void ContinueButton()
    {
        gameManager.StartGame();
    }
    public void FinishGame()
    {
        gameManager.FinishGame();
    }
    public void EndGameButton()
    {
        gameManager.EndGame();
    }
    public void SoundOnButton()
    {
        ServiceLocator.GetService<AudioManager>().ChangeSoundState(SoundState.ON);
    }
    public void SoundOffButton()
    {
        ServiceLocator.GetService<AudioManager>().ChangeSoundState(SoundState.OFF);
    }
    public void MusicOffButton()
    {
        audioManager.ChangeMusicState(SoundState.OFF);
    }
    public void MusicOnButton()
    {
        audioManager.ChangeMusicState(SoundState.ON);
    }
    public void VibroOnButton()
    {
        ServiceLocator.GetService<VibrationManager>().ChangeVibroState(VibroState.ON);
    }
    public void VibroOffButton()
    {
        ServiceLocator.GetService<VibrationManager>().ChangeVibroState(VibroState.OFF);
    }
    public void NextButton()
    {
        isWin = false;
        uiPanels[2].SetActive(false);        
    }
    public void CloseApp()
    {
        Application.Quit();
    }
}
