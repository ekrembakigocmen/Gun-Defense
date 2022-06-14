using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;


    public void PausedBtn()
    {

        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ContunieBtn()
    {

        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
