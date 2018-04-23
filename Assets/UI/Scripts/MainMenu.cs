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
    private void Start()
    {
        ShowMenu();
    }

    public void ShowMenu()
    {
        gameStarted = false;
        scoreScreen.SetActive(false);
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
