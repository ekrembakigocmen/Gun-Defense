using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public void Retry()
    {

        Time.timeScale = 1f;
        WaveSpawner.IsEnemies = 0;
        WaveSpawner.won = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void Menu()
    {
        Time.timeScale = 1f;
        WaveSpawner.EnemiesAlive = 0;
        SceneManager.LoadScene("MenuScene");

    }
}
