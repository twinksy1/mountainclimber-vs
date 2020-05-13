using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//AM 05-08-20: updated script to allow the main menu to load any scene
public class MainMenu : MonoBehaviour
{
    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void Quit ()
    {
        Application.Quit();
    }
}
