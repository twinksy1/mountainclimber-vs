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
    public int p1Score;
    //public int p2Score;

    // References main camera
    public Camera cam;

    // Score offset
    public const float scoreOffset = 5.6f;

    // Max distance player can be from cam before being considered out of bounds
    public const float verticalMaxDist = 9.0f;
    public const float horizontalMaxDist = 15.0f;

    public Text score;
    public GameObject gameover_ui;

    void Start()
    {
        gameover_ui.SetActive(false);
    }

    void Update()
    {
        // Update score every frame
        p1Score = (int)(player1.transform.position.y - scoreOffset);
        //p2Score = (int)(player2.transform.position.y - scoreOffset);

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

        score.text = "Player 1: " + p1Score + "\nPlayer 2: ";
    }

    IEnumerator delayTilRestart()
    {
        // Display some death message
        gameover_ui.SetActive(true);

        // Wait for 5 seconds
        yield return new WaitForSeconds(5);
        // Restart the scene
        restartScene();
    }

    public void restartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
