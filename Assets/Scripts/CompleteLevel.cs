using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CompleteLevel : MonoBehaviour
{
    AccountStats accountStats = new AccountStats();
    public JsonController jsonController;
    private string jsonString = "/Saves/JsonStats.json";
    public string menuSceneName = "MenuScene";
    public string nextLevel = "GameLevel02";
    public SceneFader sceneFader;
    public int requirementKey = 5;
    [HideInInspector] public Button continueBtn;

    private void Start()
    {
        load();
        if (accountStats.mapLevel > 4)
        {
            if (accountStats.advKeys >= requirementKey && accountStats.mapLevel - accountStats.levelReached > 0)
            {

                jsonController.JsonSaveAdv(-requirementKey);
                jsonController.JsonSaveReached(1);
                sceneFader.FadeTo(nextLevel);
                WaveSpawner.won = 0;

            }
            else
            {
                continueBtn.interactable = false;
                WaveSpawner.won = 0;
            }
        }
    }
    public void Continue()
    {
        sceneFader.FadeTo(nextLevel);
    }
    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }

    public void load()
    {


        if (File.Exists(Application.dataPath + jsonString))
        {
            string jsonRead = File.ReadAllText(Application.dataPath + jsonString);
            accountStats = JsonUtility.FromJson<AccountStats>(jsonRead);

        }
    }


}
