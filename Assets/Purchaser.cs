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
            case "buy.2000coins":
                AddCoins(2000);
                break;
            case "buy.5000coins":
                AddCoins(5000);
                break;
            case "buy.10000coins":
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
