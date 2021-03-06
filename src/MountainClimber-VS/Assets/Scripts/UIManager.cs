﻿// UIManager.cs - This controls the pause menu. It controls taking input from the user to activate the pause menu and pause all objects including cameras.
// Pause menu allows you to resume play from the moment the player stopped, Restart the level or exit to the main menu.
// Maintained by: Antonio-Angel Medel
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// AM 05-13-20 updated showPaused & hidePaused to pause the cameras. fixes bug where the cameras would keep going
public class UIManager : MonoBehaviour
{
    GameObject[] pauseObjects;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        hidePaused();
    }

    // Update is called once per frame
    void Update()
    {
        // AM - 05-08-20 the p button will pause and unpause the game
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                showPaused();
            }
            else if (Time.timeScale == 0)
            {
                Debug.Log("high");
                Time.timeScale = 1;
                hidePaused();
            }
        }

    }


    // AM - 04-26-20 Reload() will reload the current level that the player is at.
    // sceneToLoad will be the name of the scene that we want to load
    // This name will be added in the unity editor. It should be "ScollingTest"
    public void Reload(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    // AM - 05-08-20 pauseControl() is what actually gives the button
    // the ability to pause the scene
    public void pauseControl()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            showPaused();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            hidePaused();
        }
    }

    // AM - 05-08-20 hidePaused() will hide the pause menu by
    // setting to false all the objects with the showOnpause tag
    public void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }

        if (gameManager.cam1.GetComponent<scroll>().enabled == false)
        {
            // resume camera's and scrolling for vs mode
            gameManager.cam1.GetComponent<scroll>().enabled = true;
            gameManager.cam2.GetComponent<scroll>().enabled = true;

            gameManager.cam1.GetComponent<Camera>().enabled = true;
            gameManager.cam2.GetComponent<Camera>().enabled = true;
        }
    }

    // AM - 05-08-20 showPaused() will show the pause menu by setting
    // to true all the objects with the showOnPause tag
    public void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }

        if (gameManager.cam1.GetComponent<scroll>().enabled == true)
        {
            // stop camera's and scrolling for vs mode
            gameManager.cam1.GetComponent<scroll>().enabled = false;
            gameManager.cam2.GetComponent<scroll>().enabled = false;

            gameManager.cam1.GetComponent<Camera>().enabled = false;
            gameManager.cam2.GetComponent<Camera>().enabled = false;
        }
    }

    // JL - 5-17-20 Fixed a "sticky" settingsmanager object, now
    // deletes on returning to menu
    public void LoadLevel(string level)
    {
        if(level == "Menu")
        {
            GameObject settings;
            settings = GameObject.Find("SettingsManager");
            Destroy(settings);
        }
        SceneManager.LoadScene(level);
    }
}
