using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : Singleton<MainMenu>
{
    public GameObject menuRoot = null;
    private void Start()
    {
        ShowMenu();
    }

    public void ShowMenu()
    {
        menuRoot.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void StartGame()
    {
        menuRoot.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
