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
            gm.IntroGame();
        } else if (Input.GetButtonDown("Fire1"))
        {
            gm.StartGame();
        }

        if (Input.GetButtonDown("Fire2") && SceneManager.GetActiveScene().buildIndex != 4)
        {
            gm.QuitGame();
        }

    }
}
