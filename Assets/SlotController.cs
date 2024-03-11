using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotController : MonoBehaviour
{

    public SlotColumnController[] SlotColumns = new SlotColumnController[3];
    public Sprite[] SlotItemsTexture = new Sprite[7];
    public GameObject ColumnItemPrefab;
    public int ColumnItemHeight;
    public byte ColumnItemsCount;
    public Button SpinBt;
    public Button BetBt;
    byte columnCount = 0;
    byte[,] slotResult;
    public BetController betController;
    public TMP_Text cash;
    public int cashNum;
    public TMP_Text win;
    void Awake()
    {
        if (PlayerPrefs.HasKey("Money"))
        {
            cashNum = PlayerPrefs.GetInt("Money");
        }
        else
        {
            cashNum = 100000;
            PlayerPrefs.SetInt("Money", cashNum);
            PlayerPrefs.Save();
        }
        //Component.GetComponentInChildren<SlotColumnController>().ItemsCount = new Sprite[11];
        cash.text = cashNum.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        for (int i = 0; i < SlotColumns.Length; i++)
        {
            byte[] f = new byte[ColumnItemsCount + 4];
            for (byte g = 0; g < f.Length; g++)
            {
                f[g] = g;
            }
            SlotColumns[i].Init(SlotItemsTexture, ColumnItemsCount, ColumnItemPrefab, ColumnItemHeight, f);

        }
        
    }

    public void Spin()
    {
        LockButton();
        //logic module generate win comb.
        //call startrotation with data array
        for (int i = 0; i < SlotColumns.Length; i++)
        {
            byte[] f = new byte[ColumnItemsCount + 4];
            for (byte g = 0; g < f.Length; g++)
            {
                f[g] = (byte)Random.Range(0, SlotItemsTexture.Length - 1);
            }
            SlotColumns[i].StartRotation(f,CalculateResult);
        }
        slotResult = new byte[ColumnItemsCount + 4, SlotColumns.Length];
    }
    public void OpenMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
    void LockButton ()
    {
        SpinBt.interactable = false;
        BetBt.interactable = false;
        columnCount = 0;
    }

    void UnlockButton ()
    {
        columnCount++;
        if (columnCount >= SlotColumns.Length)
        {
            SpinBt.interactable = true;
            BetBt.interactable = true;
        }
    }

    void CalculateResult (byte[] slotResult)
    {
        bool win = false;
        for (int i = 0; i < ColumnItemsCount + 4; i++)
        {
            this.slotResult[i, columnCount] = slotResult[i];
        }
        if (columnCount >= SlotColumns.Length - 1)
        {
            for (int i = 0; i < ColumnItemsCount; i++)
            {
                if (this.slotResult[2 + i,0] == this.slotResult[2 + i, 1] && this.slotResult[2 + i, 1] == this.slotResult[2 + i, 2])
                {
                    Win();
                    win = true;
                }                
            }
            if (!win)
            {
                Lose();
            }
        }
        UnlockButton();
    }

    void Win()
    {
        cashNum += betController.betnum;
        PlayerPrefs.SetInt("Money", cashNum);
        cash.text = cashNum.ToString();
        win.text = "+" + betController.betnum.ToString();
    }

    void Lose()
    {
        cashNum -= betController.betnum;
        cash.text = cashNum.ToString();
        PlayerPrefs.SetInt("Money", cashNum);
        win.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
