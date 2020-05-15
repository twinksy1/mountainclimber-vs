// UIManagerSingle.cs - is a derivative of the original UIManager script that is altered for single player. 
// This controls the pause menu. It controls taking input from the user to activate the pause menu and pause all objects including cameras.
// Pause menu allows you to resume play from the moment the player stopped, Restart the level or exit to the main menu.
// Maintained by: Antonio-Angel Medel
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// AM 05-13-20 updated showPaused & hidePaused to pause the cameras. fixes bug where the cameras would keep going
public class UIManagerSingle : MonoBehaviour
{
    GameObject[] pauseObjects;
    public GameManagerSingle gameManagerSingle;

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
        
        if (gameManagerSingle.cam1.GetComponent<scroll>().enabled == false)
        {
            // resume camera's and scrolling for single player
            gameManagerSingle.cam1.GetComponent<scroll>().enabled = true;
            gameManagerSingle.cam1.GetComponent<Camera>().enabled = true;
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

        
        if (gameManagerSingle.cam1.GetComponent<scroll>().enabled == true)
        {
            // stop camera's and scrolling for single player
            gameManagerSingle.cam1.GetComponent<scroll>().enabled = false;
            gameManagerSingle.cam1.GetComponent<Camera>().enabled = false;
        }
    }

    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }
}
