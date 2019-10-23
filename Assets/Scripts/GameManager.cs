﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene(PlayerData.Level);
    }

    public void LoseGame()
    {
        PlayerData.Level = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("Lose");
    }

    public void WinGame()
    {
        PlayerData.Level = 2;
        SceneManager.LoadScene("Win");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(PlayerData.Level);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void IntroGame() {
        SceneManager.LoadScene("Introduction");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void WinLevel()
    {
        PlayerData.Level = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            WinGame();
        } else
        {
            SceneManager.LoadScene("LevelComplete");
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(PlayerData.Level);
    }
}
