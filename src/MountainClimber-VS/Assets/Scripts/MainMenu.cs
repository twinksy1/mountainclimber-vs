// MainMenu.cs - this is the logic for the main menu. It has a function to load levels from the game, control your options, and quit. Vs and single player
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//JL 02-26-2020: creation, allows the user to start the game or exit
//AM 05-08-2020: updated script to allow the main menu to load any scene
//AM 05-13-2020 since functions were single lines I updated them using arrow syntax
public class MainMenu : MonoBehaviour
{
    public void LoadLevel(string level) => SceneManager.LoadScene(level);

    public void Quit() => Application.Quit();
}
