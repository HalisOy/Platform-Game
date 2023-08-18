using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{
    private GameData GameData;
    private int Coin;
    public int LevelData;
    public Sprite StarSprite;
    public List<int> Test;
    private Animator CutScene;

    private void Start()
    {
        CutScene = transform.GetChild(transform.childCount - 1).GetComponent<Animator>();
        if (SaveSystem.SaveExists())
        {
            GameData = SaveSystem.LoadGame();
            Coin = GameData.Coin;
            LevelData = GameData.Level;
            Test = GameData.LevelStar;
        }
        else
        {
            GameData = new GameData(1, 0, 0, 5, new List<int>());
            Coin = 0;
            LevelData = 1;
            SaveSystem.SaveGame(GameData);
        }
        Transform LevelMenu = transform.Find("LevelMenu");
        for (int i = 0; i < LevelData; i++)
        {
            LevelMenu.Find("Tab1").GetChild(i).GetComponent<CanvasGroup>().alpha = 1;
            LevelMenu.Find("Tab1").GetChild(i).GetComponent<Button>().interactable = true;
            LevelMenu.Find("Tab1").GetChild(i).Find("Lock").gameObject.SetActive(false);
            if (LevelData > 1 && i != LevelData - 1)
                for (int j = 1; j < GameData.LevelStar[i] + 1; j++)
                {
                    LevelMenu.Find("Tab1").GetChild(i).Find("Star" + j).GetComponent<Image>().sprite = StarSprite;
                }
        }
        if (LevelData > 15)
        {
            for (int i = 0; i < LevelData; i++)
            {
                LevelMenu.Find("Tab2").GetChild(i).GetComponent<CanvasGroup>().alpha = 1;
                LevelMenu.Find("Tab2").GetChild(i).GetComponent<Button>().interactable = true;
                LevelMenu.Find("Tab2").GetChild(i).GetComponent<GameObject>();
                if (LevelData > 1 && i != LevelData - 1)
                    for (int j = 1; j < GameData.LevelStar[i] + 1; j++)
                    {
                        LevelMenu.Find("Tab2").GetChild(i).Find("Star" + j).GetComponent<Image>().sprite = StarSprite;
                    }
            }
        }
    }
    public void ChangeScene(string LoadScene)
    {
        StartCoroutine(ChangeSceneDelay(LoadScene));
    }

    private IEnumerator ChangeSceneDelay(string LoadScene)
    {
        if (LoadScene != "")
        {
            CutScene.SetTrigger("Start");
            yield return new WaitForSeconds(1f);

            SceneManager.LoadScene(LoadScene);
        }
        else
            Debug.Log("Çok Yakýnda Hizmetinizde.");
    }
}
