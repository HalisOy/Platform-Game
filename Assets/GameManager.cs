using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject ControlUI;
    [SerializeField] private GameObject PauseUI;
    [SerializeField] private GameObject DiedUI;
    [SerializeField] private GameObject FinishUI;
    [SerializeField] private GameObject PauseButton;
    [SerializeField] private GameObject JumpButton;
    private AdsGameInSystem adsGameInSystem;
    private CoinSystem GameCoin;
    public GameData GameData;
    public int LevelData;
    private int TotalCoin;
    private int Star;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        TotalCoin = GameObject.FindGameObjectsWithTag("Coin").Length;
        GameCoin = new CoinSystem();
        adsGameInSystem = GameObject.FindGameObjectWithTag("AdsSystem").GetComponent<AdsGameInSystem>();
        GameData = SaveSystem.LoadGame();
        LevelData = GameData.Level;
    }

    public void Pause()
    {
        if (Time.timeScale == 1 && ControlUI.activeSelf && !PauseUI.activeSelf)
        {
            Time.timeScale = 0;
            ControlUI.SetActive(false);
            PauseButton.SetActive(false);
            JumpButton.SetActive(false);
            PauseUI.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            ControlUI.SetActive(true);
            PauseButton.SetActive(true);
            JumpButton.SetActive(true);
            PauseUI.SetActive(false);
        }
    }

    public void Died()
    {
        ControlUI.SetActive(false);
        PauseButton.SetActive(false);
        JumpButton.SetActive(false);
        DiedUI.SetActive(true);
        if (!adsGameInSystem.interstitialAdShow)
        {
            adsGameInSystem.ShowInterstitialAd();
        }
    }

    public void Finish()
    {
        Star = 1;
        if (TotalCoin * 0.5f <= GameCoin.Coin)
            Star = Mathf.Clamp(Star + 1, 0, 3);
        if (TotalCoin == GameCoin.Coin)
            Star = Mathf.Clamp(Star + 1, 0, 3);

        for (int i = 3; i < Star + 3; i++)
        {
            FinishUI.transform.Find("Stars").GetChild(i).gameObject.SetActive(true);
        }
        string LevelName = SceneManager.GetActiveScene().name;
        int Level = Convert.ToInt32(LevelName.Substring(5, 1));
        if (GameData.LevelStar.Count < Level)
            GameData.LevelStar.Add(Star);
        else
        {
            if (GameData.LevelStar[Level - 1] < Star)
                GameData.LevelStar[Level - 1] = Star;
        }
        Level++;
        if (GameData.Level < Level)
            GameData.Level = Level;
        GameData.Coin += GameCoin.Coin;
        SaveSystem.SaveGame(GameData);
        ControlUI.SetActive(false);
        PauseButton.SetActive(false);
        JumpButton.SetActive(false);
        FinishUI.SetActive(true);
        if (!adsGameInSystem.interstitialAdShow)
        {
            adsGameInSystem.ShowInterstitialAd();
        }
    }
    public void CoinCollect()
    {
        GameCoin.CoinCollect();
    }

    public void LoadGame()
    {
        GameData = SaveSystem.LoadGame();
    }
}
