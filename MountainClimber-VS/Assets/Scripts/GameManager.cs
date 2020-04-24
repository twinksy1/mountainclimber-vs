using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
// Maintained by: Juan Villasenor
// AM: 04-20-20 tried to make a few names more human readable
public class GameManager : MonoBehaviour
{
    // References player objects
    public GameObject player1;
    //public GameObject player2;

    // Scores
    public int p1Score = 0;
    public int p2Score = 0;
    // Bonus Scores from breaking crates
    public int p1BonusScore = 0;
    public int p2BonusScore = 0;

    // References main camera
    public Camera cam;

    // Score offset
    public const float scoreOffset = 5.6f;

    // Max distance player can be from cam before being considered out of bounds
    public const float verticalMaxDist = 9.0f;
    public const float horizontalMaxDist = 15.0f;

    // UI Mechanics
    public float secondsTilRestart = 2f;
    public TextMeshProUGUI score;
    public GameObject gameover_ui;
    public TextMeshProUGUI gameover_text;
    string[] gameover_displays = new string[4] { "Gameover!\nOOOFFFF", "Gameover!\nBetter luck next time :O\n", "!gAmeOVer?\n", "At least you tried :)\n"};
    int show;

    void Start()
    {
        gameover_ui.SetActive(false);
        show = Random.Range(0, 4);
    }

    void Update()
    {
        // Update scores every frame
        int new_player1_score = (int)(player1.transform.position.y - scoreOffset);
        if (new_player1_score > p1Score)
        {
            p1Score = new_player1_score;
        }

        //int new_p2Score = (int)(player2.transform.position.y - scoreOffset);
        //if (new_p2Score > p2Score) p2Score = new_p2Score;

        // Player 1

        //float ydist = player1.transform.position.y - cam.transform.position.y;
        float xdist = Mathf.Abs(player1.transform.position.x - cam.transform.position.x);

        if(player1.transform.position.y <= cam.transform.position.y-verticalMaxDist || xdist > horizontalMaxDist)
        {
            // Player 1 is out of bounds
            Debug.Log("Player 1 is out of bounds");
            // Stop scrolling
            cam.GetComponent<scroll>().enabled = false;
            // Stop Player 1 movement
            player1.GetComponent<PlayerMovement>().enabled = false;

            StartCoroutine(DelayTilRestart());
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

        // Players broke crates, reward with bonus points
        if(player1.GetComponent<PlayerMovement>().checkBonus())
        {
            p1BonusScore += 10;
        }
        /*
         * if(player2.GetComponent<PlayerMovement>().checkBonus())
         * {
         *  p2BonusScore += 10;
         * }
         */
        /*
       if(p1Score > p2Score)
       {
           score.text = "Player 1 is beating Player 2 :O\nPlayer 1: " + (p1Score+p1BonusScore) + "\nPlayer 2: " + (p2Score+p2BonusScore);
       } else if(p2Score > p1Score)
       {
           score.text = "Player 2 is owning Player 1 :O\nPlayer 1: " + (p1Score+p1BonusScore) + "\nPlayer 2: " + (p2Score+p2BonusScore);
       } else
       {
           score.text = "The race is neck to neck :O\nPlayer 1: " + (p1Score+p1BonusScore) + "\nPlayer 2: " + (p2Score+p2BonusScore);
       }
       */

        score.text = "Player 1: " + (p1Score + p1BonusScore) + "\nPlayer 2: " + (p2Score + p2BonusScore);
    }

    IEnumerator DelayTilRestart()
    {
        gameover_text.text = gameover_displays[show] + "\n\nPlayer 1: " + (p1Score+p1BonusScore).ToString() + "\nPlayer 2: " + (p2Score+p2BonusScore).ToString();

        // Display some death message
        gameover_ui.SetActive(true);
        score.enabled = false;

        // Wait
        yield return new WaitForSeconds(secondsTilRestart);

        // Restart the scene
        RestartScene();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
