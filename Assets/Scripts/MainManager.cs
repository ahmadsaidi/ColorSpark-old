using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && SceneManager.GetActiveScene().buildIndex == 0)
        {
            gm.StartGame();
        } else if (Input.GetButtonDown("Fire1") && SceneManager.GetActiveScene().name == "Introduction")
        {
            gm.StartGame();
        }
         else if (Input.GetButtonDown("Fire2") && SceneManager.GetActiveScene().name == "Introduction")
        {
            gm.MainMenu();
        }
        else if (Input.GetButtonDown("Fire1") && SceneManager.GetActiveScene().name == "LevelComplete")
        {
            gm.NextLevel();
        } else if (Input.GetButtonDown("Fire1") && SceneManager.GetActiveScene().name == "Lose")
        {
            gm.RetryLevel();
        }

        if (Input.GetButtonDown("Fire2") && SceneManager.GetActiveScene().name == "StartMenu")
        {
            gm.QuitGame();
        }
        else
        {
            gm.MainMenu();
        }

    }
}
