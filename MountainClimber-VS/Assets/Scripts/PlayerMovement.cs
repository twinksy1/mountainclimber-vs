using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

//PM 04-23-2020: Added a boolean variable and two functions that help correlate the bonus points recieved from breaking crates
//              Did not remove or alter any other code - Juan

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
    bool locationLock = false;

    private bool recieveBonus = false;

    public void OnLanding()
    {
        animator.SetBool("IsJump", false);
        animator.SetBool("LandFrame", false);
        animator.SetBool("IsFalling", false);
    }

    // Update is called once per frame
    void Update(){
        bool vectorbool = controller.getVectorBoolY();
       
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if(horizontalMove != 0)
        {
            locationLock = true;
        }
        else
        {
            locationLock = false;
        }

        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJump", true);
        }

        if (Input.GetButtonDown("Up") && locationLock == false)
        {
            lookUp = true;
            animator.SetBool("IsLookUp", true);
        }
        else if (Input.GetButtonUp("Up"))
        {
            lookUp = false;
            animator.SetBool("IsLookUp", false);
        }

        if (Input.GetButtonDown("Down") && locationLock == false)
        {
            lookDown = true;
            animator.SetBool("IsLookDown", true);
        } else if(Input.GetButtonUp("Down"))
        {
            lookDown = false;
            animator.SetBool("IsLookDown", false);
        }

        if(vectorbool == true)
        {
            animator.SetBool("IsFalling", false);
        }
        else
        {
            animator.SetBool("IsJump", false);
            animator.SetBool("IsFalling", true);
        }

    }


    void FixedUpdate()
    {
        //Character Movement
        controller.Move(horizontalMove*Time.fixedDeltaTime, jump);
        jump = false;
    }


    // Bonus Score Points
    public void setBonusPoints()
    {
        recieveBonus = true;
    }

    public bool checkBonus()
    {
        if (recieveBonus == true)
        {
            recieveBonus = false;
            return true;
        }
        else
            return false;
    }
}
