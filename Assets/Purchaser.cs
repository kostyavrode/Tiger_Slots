using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
public class Purchaser : MonoBehaviour
{
    public void OnPurchaseCompleted(Product product)
    {
        switch(product.definition.id)
        {
            case "com.2000.buycoins":
                AddCoins(2000);
                break;
            case "com.5000.buycoins":
                AddCoins(5000);
                break;
            case "com.10000.buycoins":
                AddCoins(10000);
                break;
        }
        Debug.Log(product.definition.id);
    }
    private void AddCoins(int count)
    {
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + count);
        PlayerPrefs.Save();
        UIManager.instance.ShowMoney(PlayerPrefs.GetInt("Money").ToString());
    }
}
