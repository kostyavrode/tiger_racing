using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Gate> gates;
    public int levelNum;
    private UIManager uiManager;
    private TMP_Text gameTimeBar;
    private bool isLevelEnded;
    private int reachedGates;
    public int gameTime;
    private float spendedTime;
    private void Awake()
    {
        foreach (var gate in gates)
        {
            gate.InitGate(this);
        }
        uiManager=ServiceLocator.GetService<UIManager>();
        gameTimeBar = uiManager.timeBar;
        if (PlayerPrefs.HasKey("Buy1"))
        {
            gameTime += 30;
        }
    }
    private void Update()
    {
        spendedTime += Time.deltaTime;
        gameTimeBar.text = (gameTime - Math.Round(spendedTime)).ToString();
        //if (spendedTime > gameTime)
        //{
        //    LevelEnded(false);
        //}
    }
    public void DisableGate(Gate gate)
    {
        reachedGates += 1;
        if (gates.Count == 0 || gates[gates.Count - 1] == gate)
        {

            LevelEnded(true);
        }
        gates.Remove(gate);

        //Destroy(gate.gameObject);

    }
    public void LevelEnded(bool isWin)
    {
        if (!isLevelEnded)
        {
            uiManager.isWin = isWin;
            if (isWin)
            {
                PlayerPrefs.SetInt(("LevelDone" + levelNum), 3);
                PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 120);
                PlayerPrefs.Save();
                uiManager.ShowMoney();
                Debug.Log("LevelComplete");

            }
            else
            {
                Debug.Log("Failed");
            }
            isLevelEnded = true;
            spendedTime = 0;
            reachedGates = 0;
            uiManager.FinishGame();
            Destroy(gameObject);
            
        }
    }
}
