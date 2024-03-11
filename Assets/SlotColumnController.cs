using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotColumnController : MonoBehaviour
{
    public float TimeRotation = 5.5f;
    Sprite[] SlotItemsTexture; // текустуры элементов
    byte SloteItemsCount; // количество видимых элементов в столбце
    byte[] ColumnResult; // комбинация которая должна появится в результате вращения +2 снизу и +2 сверху
    Image[] columnItems; // список префабов-элементов столбца
    int ColumnHeight;
    float speed = 0;
    float timeRotation = 0;
    byte replaceCount = 0;
    public bool stop = false;
    bool spin = false;
    public delegate void CalculateResult(byte[] slotResult);
    CalculateResult calculateResult;
    public void Init (Sprite[] slotItemsTexture, byte slotItemsCount, GameObject columnItemPrefab, int columnHeight, byte[] columnResult)
    {
        speed = 0;
        ColumnHeight = columnHeight;
        Image _item;
        SlotItemsTexture = slotItemsTexture;
        SloteItemsCount = slotItemsCount;
        columnItems = new Image[slotItemsCount + 4];
        for (int i = 0; i < slotItemsCount +4; i++)
        {
            _item = Instantiate(columnItemPrefab, new Vector3(0, 0, -3), Quaternion.identity).GetComponent<Image>();
            _item.rectTransform.sizeDelta = new Vector2(180, 180);
            _item.transform.SetParent(this.transform);
            //_item.rectTransform.position = new Vector3(0, , 0);
            _item.rectTransform.anchoredPosition = new Vector2(0, columnHeight / slotItemsCount * -Mathf.FloorToInt((slotItemsCount + 4) / 2) + i * columnHeight / slotItemsCount);
            _item.sprite = slotItemsTexture[Random.Range(0, slotItemsTexture.Length)];
            columnItems[i] = _item;
        }
        ColumnResult = columnResult;
    }

    public void StartRotation(byte[] columnResult, CalculateResult calculateResult) // начинает вращение столбца, columnResult - результат вращения столбца
    {
        //start free animation
        //get result
        spin = true;
        ColumnResult = columnResult;
        stop = false;
        timeRotation = 0;
        replaceCount = 0;
        speed = 0;
        this.calculateResult = calculateResult;

    }

    void AnimationRotationStart(float timeRotation)
    {
        int _itemsCount = columnItems.Length - 4;
        int _deltaHeight = ColumnHeight / _itemsCount;
        if (replaceCount < _itemsCount + 4)
        for (int i = 0; i < columnItems.Length; i++)
        {
            columnItems[i].transform.Translate(new Vector3(0, -speed, 0));
            if (columnItems[i].rectTransform.anchoredPosition.y < -_deltaHeight * Mathf.FloorToInt((_itemsCount + 4) / 2) )
            {
                columnItems[i].rectTransform.anchoredPosition = new Vector2(0, _deltaHeight * (Mathf.FloorToInt((_itemsCount + 4) / 2) * 2 + 1) + columnItems[i].rectTransform.anchoredPosition.y);
                if (stop == true)
                {
                    columnItems[i].sprite = SlotItemsTexture[ColumnResult[replaceCount]];
                    replaceCount++;
                }
                else
                columnItems[i].sprite = SlotItemsTexture[Random.Range(0, SlotItemsTexture.Length - 1)];
            }
        }
        else
        {
            float p = columnItems[0].rectTransform.anchoredPosition.y / (_deltaHeight);
            float pos;
            if (p >= 0)
            {
                pos = (p - (int)p) * _deltaHeight;
            }
            else
            {
                pos = (p - (int)p + 1) * _deltaHeight;
            }
            if (pos > speed)
            {
                for (int i = 0; i < columnItems.Length; i++)
                    columnItems[i].transform.Translate(new Vector3(0, -speed, 0));
            }
            else
            {
                if (pos > 5)
                for (int i = 0; i < columnItems.Length; i++)
                    columnItems[i].transform.Translate(new Vector3(0, -pos, 0));
                spin = false;
                calculateResult(ColumnResult);
            }

        }
        if (speed < 50)
        {
            speed = speed + 0.25f;
        }
        this.timeRotation += Time.deltaTime;
        if (this.timeRotation > TimeRotation)
        {
            stop = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (spin)
            AnimationRotationStart(timeRotation);
    }
}
