using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject[] shopEntities;
    public GameObject shopPanel;
    public MeshRenderer[] planeModel;
    public Material[] materials;
    private int currentEntityNum;
    private void Awake()
    {
        CheckMat();
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
    public void SetColor(int colorNum)
    {
        if (PlayerPrefs.GetInt("Money") >= 120)
        {
            Material randomMaterial = materials[colorNum];
            foreach (MeshRenderer mesh in planeModel)
            {
                mesh.material = randomMaterial;
            }
            PlayerPrefs.SetInt("Mat", colorNum);
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 120);
            PlayerPrefs.Save();
            ServiceLocator.GetService<UIManager>().ShowMoney();
        }
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
        if (PlayerPrefs.HasKey("Mat"))
        {

        
        Material randomMaterial = materials[PlayerPrefs.GetInt("Mat")];
            Debug.Log("Nat" + PlayerPrefs.GetInt("Mat"));
            foreach (MeshRenderer mesh in planeModel)
            {
                mesh.material = randomMaterial;

            }
        }
        gameObject.SetActive(false);
    }
    public void GetRandomColor()
    {
        if (PlayerPrefs.GetInt("Money")>120)
        {
            int rand = Random.Range(0, materials.Length);
            Material randomMaterial = materials[rand];
            foreach (MeshRenderer mesh in planeModel)
            {
                mesh.material = randomMaterial;
            }
            PlayerPrefs.SetInt("Mat", rand);
            PlayerPrefs.SetInt("Money",PlayerPrefs.GetInt("Money") - 120);
            PlayerPrefs.Save();
            ServiceLocator.GetService<UIManager>().ShowMoney();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.SetInt("Money", 1000);
        }
    }
}
