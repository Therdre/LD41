using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CharacterNameSpace;
using FoodNameSpace;

public class MainMenu : Singleton<MainMenu>
{
    public GameObject menuRoot = null;

    //score screen
    public GameObject scoreScreen = null;
    public Text scoreText = null;
    public bool gameStarted = false;

    [Header("In game menu")]
    //in game menu
    public GameObject inGameMenu = null;

    private void Start()
    {
        ShowMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameStarted)
        {
            inGameMenu.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }
    
    public void Continue()
    {
        scoreScreen.SetActive(false);
        inGameMenu.SetActive(false);
        menuRoot.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void ShowMenu()
    {
        gameStarted = false;
        scoreScreen.SetActive(false);
        inGameMenu.SetActive(false);
        menuRoot.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void ShowScoreScreen(int score)
    {
        gameStarted = false;
        scoreScreen.SetActive(true);
        scoreText.text = "Recipes completed: " + score;
    }

    public void StartGame()
    {
        inGameMenu.SetActive(false);
        scoreScreen.SetActive(false);
        menuRoot.SetActive(false);
        Time.timeScale = 1.0f;

        CharacterTurnManager.Instance.ResetGame();
        FoodInventory.Instance.ResetGame();
        RecipeManager.Instance.ResetGame();
        gameStarted = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
