using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

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


    public void OnLanding()
    {
        animator.SetBool("IsJump", false);
        animator.SetBool("LandFrame", false);
    }

    // Update is called once per frame
    void Update(){
       
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
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
