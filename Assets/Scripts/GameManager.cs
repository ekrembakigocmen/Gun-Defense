using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    
    JsonController jsonController;
    public int levelUp = 2;
    public static bool GameIsOver;
    public GameObject gameOverUI;
    public GameObject completeLevelUI;
    public SceneFader sceneFader;
    private void Start()
    {

        GameIsOver = false;
        jsonController = GetComponent<JsonController>();
    }
    void Update()
    {
        if (GameIsOver)
            return;

        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
        Invoke("Freeze", .5f);
    }

    public void WinLevel()
    {
        
        GameIsOver = true;
        completeLevelUI.SetActive(true);
        jsonController.JsonSave(levelUp);
        
    }
    private void Freeze()
    {
        Time.timeScale = 0;
    }
}
