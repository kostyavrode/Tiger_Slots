using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetController : MonoBehaviour
{
    public TMPro.TMP_Text bettext;
    public int betnum = 50;
    public int betmin=50;
    public int betmax = 6401;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void BetDown()
    {
        betnum = betnum / 2;
        if (betnum < betmin)
            betnum = 50;
        bettext.text = betnum.ToString();
    }
    public void BetUp()
    {
        betnum = betnum * 2;
        if (betnum > betmax)
            betnum = 50;
        bettext.text = betnum.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
