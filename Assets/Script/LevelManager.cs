using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Gate> gates;
    [SerializeField] private List<GameObject> cars;
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
        //if (PlayerPrefs.HasKey("Buy1"))
        //{
        //    gameTime += 30;
        //}
        int tempCar = PlayerPrefs.GetInt("Car");
        cars[tempCar].SetActive(true);
    }
    private void Start()
    {
        DeactivateGates();
    }
    private void Update()
    {
        spendedTime += Time.deltaTime;
        gameTimeBar.text = (gameTime - Math.Round(spendedTime)).ToString();
        if (spendedTime > gameTime)
        {
            LevelEnded(false);
        }
    }
    public void DeactivateGates()
    {
        for (int i = 0;i < gates.Count;i++)
        {
            if (i==0)
            {
                gates[i].gameObject.SetActive(true);
            }
            else
            {
                gates[i].gameObject.SetActive(false);
            }
        }
    }
    public void DisableGate(Gate gate)
    {
        
        Debug.Log(reachedGates);
        if (gates.Count == 0 || gates[gates.Count - 1] == gate)
        {

            LevelEnded(true);
        }
        gates.Remove(gate);
        gates[reachedGates].gameObject.SetActive(true);
        //reachedGates += 1;
        //

        Destroy(gate.gameObject);

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
                uiManager.ShowWinTime(Math.Round(spendedTime).ToString());
            }
            else
            {
                Debug.Log("Failed");
                PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 12);
            }
            isLevelEnded = true;
            spendedTime = 0;
            reachedGates = 0;
            uiManager.FinishGame();
            
            Destroy(gameObject);
            
        }
    }
}
