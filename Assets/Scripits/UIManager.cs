using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject orientator;
    [SerializeField] private TMP_Text scoreBar;
    [SerializeField] private TMP_Text bestScoreBar;
    [SerializeField] private TMP_Text moneyBar;
    [SerializeField] private GameObject soundButtonOn;
    [SerializeField] private GameObject soundButtonOff;
    [SerializeField] private AudioSource audioSource;
    private UniWebView uniWebView;
    [SerializeField] public GameObject[] elements;
    [SerializeField] private GameObject blackWindow;
    [SerializeField] private AudioSource source;
    [SerializeField] public GameObject inGameUI;
    [SerializeField] private GameObject closeWebviewButton;
    public GameObject startMenuUI;
    public GameObject loseUI;
    public GameObject winUI;
    private void Awake()
    {
        instance = this;
        if (!PlayerPrefs.HasKey("Sound"))
        {
            PlayerPrefs.SetString("Sound", "true");
            PlayerPrefs.Save();
        }
        CheckSound();
        if (!PlayerPrefs.HasKey("Privacy"))
        {
            ShowFirstPrivacy();
            closeWebviewButton.SetActive(true);
        }
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            CloseUI();
        }
    }
    public void CloseUI()
    {
        source.Pause();
        foreach (GameObject obj in elements)
        {
            obj.SetActive(false);
        }
        blackWindow.SetActive(true);
        orientator.SetActive(true);
    }
    public void EndGame(bool isWin=false)
    {
        inGameUI.SetActive(false);
        if (isWin)
        {
            winUI.SetActive(true);
        }
        else
        {
            loseUI.SetActive(true);
        }
    }
    public void StartGame(int level)
    {
        GameManager.instance.StartGame(level);
    }
    public void CheckSound()
    {
        if (PlayerPrefs.GetString("Sound")=="true")
        {
            audioSource.Play();
            soundButtonOff.SetActive(true);
            soundButtonOn.SetActive(false);
        }
        else
        {
            audioSource.Pause();
            soundButtonOff.SetActive(false);
            soundButtonOn.SetActive(true);
        }
    }
    public void SoundOff()
    {
        PlayerPrefs.SetString("Sound", "false");
        PlayerPrefs.Save();
    }
    public void SoundOn()
    {
        PlayerPrefs.SetString("Sound", "true");
        PlayerPrefs.Save();
    }
    public void ShowMoney(string money)
    {
        moneyBar.text = money;
    }
    public void PauseGame()
    {
        GameManager.instance.PauseGame();
    }
    public void UnPauseGame()
    {
        GameManager.instance.UnPauseGame();
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ShowScore(string score)
    {
        scoreBar.text = score;
    }
    public void ShowBestScore(string bestScore)
    {
        //bestScoreBar.text = bestScore;
    }
    public void ShowPrivacy(string url)
    {
        var webviewObject = new GameObject("UniWebview");
        uniWebView = webviewObject.AddComponent<UniWebView>();
        uniWebView.Frame = new Rect(0, 0, Screen.width, Screen.height);
        uniWebView.SetShowToolbar(true, false, true, true);
        uniWebView.Load(url);
        uniWebView.Show();
    }
    public void ShowFirstPrivacy()
    {
        var webviewObject = new GameObject("UniWebview");
        webviewObject.tag = "WV";
        uniWebView = webviewObject.AddComponent<UniWebView>();
        uniWebView.Frame = new Rect(Screen.width / 4, Screen.height / 4, Screen.width/2, Screen.height/2);
        uniWebView.SetShowToolbar(false, false, true, true);
        uniWebView.Load("https://www.privacypolicyonline.com/live.php?token=9DLp8I1N6i40UVMEASRxTKVHbPxhF7QL");
        uniWebView.Show();
    }
    public void DestroyWebview()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("WV");
        Destroy(obj);
        PlayerPrefs.SetString("Privacy","true");
        PlayerPrefs.Save();
    }
}
