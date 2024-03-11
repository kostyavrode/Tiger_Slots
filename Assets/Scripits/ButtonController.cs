using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class ButtonController : MonoBehaviour
{
    public Button button;
    public GameObject stars;
    public Sprite openSprite;
    public Sprite closeSprite;
    public TMP_Text levelNumber;
    private void OnEnable()
    {
        Int32.TryParse(levelNumber.text,out int levelNum);
        Debug.Log(levelNum);
        if (PlayerPrefs.HasKey(("LevelDone") + levelNum) || levelNum==1)
        {
            //stars.SetActive(true);
            button.interactable = true;
            button.GetComponent<Image>().sprite = openSprite;
        }
        else
        {
            button.interactable = false;
            button.GetComponent<Image>().sprite = closeSprite;
        }
    }
    public void OpenScene(string t)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(t);
    }
}
