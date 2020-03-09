using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

// AM 03-09-20 - added some clarifying comments on variables and functions

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;
    public System.Timers.Timer _delayTimer;

    float horizontalMove = 0f;
    float verticalMove = 0f;
    bool jump = false;
    bool lookDown = false;
    bool lookUp = false;

    // AM 03-09-20 - ??? This looks like a
    // function that might only be called at the start of the game
    public void OnLanding()
    {
        animator.SetBool("IsJump", false);
        animator.SetBool("LandFrame", false);
    }

    // Update is called once per frame
    void Update(){
        // AM 03-09-20 - horizonalMove gets the raw input of the player
        // (left is -1 right is 1) * the run speed to control how fast the player moves
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        // AM 03-09-20 - .GetButtonDown("button name")
        // returns true when the player presses on the given button
        // AM 03-09-20 - .GetButtonUp(" button name ")
        // returns true if the player releases the given button
        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJump", true);
        }

        if (Input.GetButtonDown("Up"))
        {
            lookUp = true;
            animator.SetBool("IsLookUp", true);
        }
        else if (Input.GetButtonUp("Up"))
        {
            lookUp = false;
            animator.SetBool("IsLookUp", false);
        }

        if (Input.GetButtonDown("Down"))
        {
            lookDown = true;
            animator.SetBool("IsLookDown", true);
        } else if(Input.GetButtonUp("Down"))
        {
            lookDown = false;
            animator.SetBool("IsLookDown", false);
        }
    }


    void FixedUpdate()
    {
        /* AM 03-08-20 - Move character by a certain amount (horizontalMove)
         multiply it by the amount of time that has elapsed
         sense the last time fixedupdate was called. This
         ensures that the movement is the same no matter
         the amount of times FixedUpdate was called */
        controller.Move(horizontalMove*Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
