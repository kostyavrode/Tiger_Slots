using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public int cost1;
    public int cost2;
    public int cost3;
    public GameObject buyButton1;
    public GameObject buyButton2;
    public GameObject buyButton3;
    private void OnEnable()
    {
        CheckBuy();
    }
    private void CheckBuy()
    {
        if (PlayerPrefs.HasKey("Buy1"))
        {
            buyButton1.SetActive(false);
        }
        if (PlayerPrefs.HasKey("Buy2"))
        {
            buyButton2.SetActive(false);
        }
        if (PlayerPrefs.HasKey("Buy3"))
        {
            buyButton2.SetActive(false);
        }
    }
    public void Buy1()
    {
        if (PlayerPrefs.GetInt("Money")>=cost1)
        {
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - cost1);
            PlayerPrefs.SetString("LevelDone2", "LevelDone2");
            PlayerPrefs.SetString("Buy1","true");
            PlayerPrefs.Save();
            CheckBuy();
        }
    }
    public void Buy2()
    {
        if (PlayerPrefs.GetInt("Money") >= cost2)
        {
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - cost2);
            PlayerPrefs.SetString("LevelDone3", "LevelDone3");
            PlayerPrefs.SetString("Buy2", "true");
            PlayerPrefs.Save();
            CheckBuy();
        }
    }
    public void Buy3()
    {
        if (PlayerPrefs.GetInt("Money") >= cost3)
        {
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - cost3);
            PlayerPrefs.SetString("LevelDone4", "LevelDone4");
            PlayerPrefs.SetString("Buy3", "true");
            PlayerPrefs.Save();
            CheckBuy();
        }
    }
}
