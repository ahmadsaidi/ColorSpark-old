using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioClip blueAudio, greenAudio, redAudio, yellowAudio;
    public AudioClip pushboxAudio, runfasterAudio, teleportAudio, blastAudio;
    public AudioClip create_teleport, door_blast, build_bridge, float_objects;
    public AudioClip spark_to_engine, jump;
    public AudioClip tut, main, puzzle1, puzzle2, win, lose, end;
    AudioSource music;
    void Start()
    {
        music = GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().name == "Introduction")
        {
            music.clip = main;
            music.Play();
        }

        if (SceneManager.GetActiveScene().name == "LevelComplete" || SceneManager.GetActiveScene().name == "Win")
        {
            music.clip = win;
            music.Play();
        }
        if( SceneManager.GetActiveScene().name == "Lose")
        {
            music.clip = lose;
            music.Play();
        }
        if (SceneManager.GetActiveScene().name == "TUTbluePU" || SceneManager.GetActiveScene().name == "TUTredPU" || 
            SceneManager.GetActiveScene().name == "TUTgreemPU" || SceneManager.GetActiveScene().name == "TUTyellowPU"||
           SceneManager.GetActiveScene().name == "powerEngineTut")
        {
            music.clip = tut;
            music.Play();

        }

        if (SceneManager.GetActiveScene().name == "Map1"|| SceneManager.GetActiveScene().name == "Level1" || SceneManager.GetActiveScene().name == "EngineLevel1")
       
        {
            music.clip = puzzle1;
            music.Play();
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
