using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // References player objects
    public GameObject player1;
    //public GameObject player2;

    // Scores
    public int p1Score = 0;
    public int p2Score = 0;

    // References main camera
    public Camera cam;

    // Score offset
    public const float scoreOffset = 5.6f;

    // Max distance player can be from cam before being considered out of bounds
    public const float verticalMaxDist = 9.0f;
    public const float horizontalMaxDist = 15.0f;

    // UI Mechanics
    public float secondsTilRestart = 2f;
    public Text score;
    public GameObject gameover_ui;
    public Text gameover_text;
    string[] gameover_displays = new string[5] { "Gameover!\nOOOFFFF", "Gameover!\nBetter luck next time :O\n", "!gAmeOVer?\n", "At least you tried :)\n", "Have a nice trip, see you next fall!\n" };
    int show;

    void Start()
    {
        gameover_ui.SetActive(false);
        show = Random.Range(0, 5);
    }

    void Update()
    {
        // Update scores every frame
        int new_p1Score = (int)(player1.transform.position.y - scoreOffset);
        if (new_p1Score > p1Score) p1Score = new_p1Score;

        //int new_p2Score = (int)(player2.transform.position.y - scoreOffset);
        //if (new_p2Score > p2Score) p2Score = new_p2Score;

        // Player 1

        float ydist = Mathf.Abs(player1.transform.position.y - cam.transform.position.y);
        float xdist = Mathf.Abs(player1.transform.position.x - cam.transform.position.x);

        if(ydist > verticalMaxDist || xdist > horizontalMaxDist)
        {
            // Player 1 is out of bounds
            Debug.Log("Player 1 is out of bounds");
            // Stop scrolling
            cam.GetComponent<scroll>().enabled = false;
            // Stop Player 1 movement
            player1.GetComponent<PlayerMovement>().enabled = false;

            StartCoroutine(delayTilRestart());
        }

        // Player 2 

        /*
        ydist = Mathf.Abs(player2.transform.position.y - cam.transform.position.y);
        xdist = Mathf.Abs(player2.transform.position.x - cam.transform.position.x);
        
        if(ydist > maxDist || xdist > maxDist)
        {
            // Player 2 is out of bounds
            Debug.Log("Player 2 is out of bounds");
        }
        */

        if(p1Score > p2Score)
        {
            score.text = "Player 1 is beating Player 2 :O\nPlayer 1: " + p1Score + "\nPlayer 2: " + p2Score;
        } else if(p2Score > p1Score)
        {
            score.text = "Player 2 is owning Player 1 :O\nPlayer 1: " + p1Score + "\nPlayer 2: " + p2Score;
        } else
        {
            score.text = "The race is neck to neck :O\nPlayer 1: " + p1Score + "\nPlayer 2: " + p2Score;
        }

    }

    IEnumerator delayTilRestart()
    {
        gameover_text.text = gameover_displays[show] + "\n\nPlayer 1: " + p1Score.ToString() + "\nPlayer 2: " + p2Score.ToString();

        // Display some death message
        gameover_ui.SetActive(true);

        // Wait
        yield return new WaitForSeconds(secondsTilRestart);

        // Restart the scene
        restartScene();
    }

    public void restartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
