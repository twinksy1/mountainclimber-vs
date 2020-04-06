using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public const float maxDist = 9.0f;

    void Update()
    {
        // Update score every frame
        p1Score = (int)(player1.transform.position.y - scoreOffset);
        //p2Score = (int)(player2.transform.position.y - scoreOffset);

        //Check if either player is out of bounds
        float ydist = Mathf.Abs(player1.transform.position.y - cam.transform.position.y);
        float xdist = Mathf.Abs(player1.transform.position.x - cam.transform.position.x);

        if(ydist > maxDist || xdist > maxDist)
        {
            // Player 1 is out of bounds
            Debug.Log("Player 1 is out of bounds");
            // Stop scrolling
            cam.GetComponent<scroll>().enabled = false;
        }

        /*
        ydist = Mathf.Abs(player2.transform.position.y - cam.transform.position.y);
        xdist = Mathf.Abs(player2.transform.position.x - cam.transform.position.x);
        
        if(ydist > maxDist || xdist > maxDist)
        {
            // Player 2 is out of bounds
            Debug.Log("Player 2 is out of bounds");
        }
        */
    }
}
