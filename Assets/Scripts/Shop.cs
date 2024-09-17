using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject[] shopEntities;
    public GameObject[] buyButtons;
    public GameObject[] setButtons;
    public GameObject shopPanel;
    public Material[] materials;
    private int currentEntityNum;
    public int cost1;
    public int cost2;
    public int cost3;
    public int cost4;
    public GameObject notEnough;
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Buy1"))
        {
            PlayerPrefs.SetInt("Buy1", 0);
        }
        CheckMat();
    }
    private void Start()
    {
        this.gameObject.SetActive(false);
    }
    private void ShowEntity(int entityNum)
    {
        shopEntities[currentEntityNum].gameObject.SetActive(false);
        currentEntityNum = entityNum;
        //Instantiate(shopEntities[currentEntityNum]);
        shopEntities[currentEntityNum].gameObject.SetActive(true);
    }
    public void GoRight()
    {
        if (currentEntityNum <= shopEntities.Length-2)
        {
            ShowEntity(currentEntityNum+1);
        }
        else
        {
            ShowEntity(0);
        }
    }
    public void GoLeft()
    {
        Debug.Log("Go Left");
        if (currentEntityNum>0)
        {
            ShowEntity(currentEntityNum-1);
        }
        else
        {
            ShowEntity(shopEntities.Length-1);
        }

    } 
    public void BuyCar(int entityNum)
    {
        switch(entityNum)
        {
            case 0:
                {
                    if (PlayerPrefs.GetInt("Money") >= cost1)
                    {
                        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - cost1);
                        PlayerPrefs.SetInt("Buy1", 1);
                    }
                    else
                    {
                        notEnough.SetActive(true);
                        return;
                    }
                }
                break;
            case 1:
                {
                    if (PlayerPrefs.GetInt("Money") >= cost2)
                    {
                        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - cost2);
                        PlayerPrefs.SetInt("Buy2", 1);
                    }
                    else
                    {
                        notEnough.SetActive(true);
                        return;
                    }
                }
                break;
            case 2:
                {
                    if (PlayerPrefs.GetInt("Money") >= cost3)
                    {
                        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - cost3);
                        PlayerPrefs.SetInt("Buy3", 1);
                    }
                    else
                    {
                        notEnough.SetActive(true);
                        return;
                    }
                }
                break;
        }
        SetCar(entityNum);
        ServiceLocator.GetService<UIManager>().ShowMoney();
        CheckMat();
    }
    public void SetCar(int entityNum)
    {
        PlayerPrefs.SetInt("Car", entityNum);
        PlayerPrefs.Save();
    }
    public void BuyTime()
    {
        if (PlayerPrefs.GetInt("Money") >= 200)
        {
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 200);
            PlayerPrefs.SetInt("Buy1", 1);
            PlayerPrefs.Save();
            ServiceLocator.GetService<UIManager>().ShowMoney();
        }
    }
    private void CheckMat()
    {
        if (PlayerPrefs.HasKey("Buy1"))
        {
            buyButtons[0].SetActive(false);
            setButtons[0].SetActive(true);
        }
        if (PlayerPrefs.HasKey("Buy2"))
        {
            buyButtons[1].SetActive(false);
            setButtons[1].SetActive(true);
        }
        if (PlayerPrefs.HasKey("Buy3"))
        {
            buyButtons[2].SetActive(false);
            setButtons[2].SetActive(true);
        }
        if (PlayerPrefs.HasKey("Buy4"))
        {
            buyButtons[3].SetActive(false);
            setButtons[3].SetActive(true);
        }
        //cars[PlayerPrefs.GetInt("Car") - 1].SetActive(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.SetInt("Money", 1000);
        }
    }
}
