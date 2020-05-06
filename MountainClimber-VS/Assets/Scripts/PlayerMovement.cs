//JV 04-23-2020: Added a boolean variable and two functions that help correlate the bonus points recieved from breaking crates
//              Did not remove or alter any other code - Juan
//JV 04-28-2020: Added modifications for two player support - Juan
//AM 05-02-2020: Added attack animation logic using keyboard inputs and animation triggers
//JV 05-02-2020: Added player jump & land sounds
// JV 05-04-2020: Modified code for better attack animation functionality

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
    bool locationLock = false;
    private bool isAttacking = false;
    // JV 05-05-2020: Attack timer
    // How long player can have its pick out
    public float pickout_timer = 2.0f;
    private float curr_time = 0.0f;
    // JV 05-05-2020: Latching to walls
    public int latch_times = 2;
    private int latch_count;
    public bool isLatched = false;


    // Audio stuff
    public AudioSource jump_sound;
    public AudioSource land_sound;
    public float volume = 0.7f;

    public void OnLanding()
    {
        animator.SetBool("IsJump", false);
        animator.SetBool("LandFrame", false);
        animator.SetBool("IsFalling", false);
        animator.SetBool("IsAttack", false);
        // Play the land sound
        land_sound.PlayOneShot(land_sound.clip, volume);
    }

    void Start()
    {
        curr_time = pickout_timer;
        latch_count = latch_times;
    }

    // Update is called once per frame
    void Update()
    {
        // Player One
        if (this.tag == "PlayerOne")
        {
            bool vectorbool = controller.getVectorBoolY();

            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (horizontalMove != 0)
            {
                locationLock = true;
            }
            else
            {
                locationLock = false;
            }

            if (Input.GetButtonDown("Jump") && latch_count > 0)
            {
                jump = true;
                // Play the jump sound
                if(animator.GetBool("IsJump") == false)
                {
                    jump_sound.PlayOneShot(jump_sound.clip, volume);
                }
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
            }
            else if (Input.GetButtonUp("Down"))
            {
                lookDown = false;
                animator.SetBool("IsLookDown", false);
            }

            // AM 05-02-20 check to see if the animator should play the attack animation
            // JV 05-04-20: Modified attack animation
            if (Input.GetButtonDown("Attack"))
            {
                if(curr_time > 0.0f)
                {
                    animator.SetBool("IsAttack", true);
                    Debug.Log("ATTACKING");
                    isAttacking = true;
                } else
                {
                    animator.SetBool("IsAttack", false);
                    isAttacking = false;
                }
            }
            else if(Input.GetButtonUp("Attack"))
            {
                animator.SetBool("IsAttack", false);
                isAttacking = false;
                curr_time = pickout_timer;
            }

            if (vectorbool == true)
            {
                animator.SetBool("IsFalling", false);
            }
            else
            {
                animator.SetBool("IsJump", false);
                animator.SetBool("IsFalling", true);
            }
        }
        // Player Two
        else
        {
            bool vectorbool = controller.getVectorBoolY();

            horizontalMove = Input.GetAxisRaw("Horizontal1") * runSpeed;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (horizontalMove != 0)
            {
                locationLock = true;
            }
            else
            {
                locationLock = false;
            }

            if (Input.GetButtonDown("Jump1") && latch_count > 0)
            {
                jump = true;
                // Play the jump sound
                if (animator.GetBool("IsJump") == false)
                {
                    jump_sound.PlayOneShot(jump_sound.clip, volume);
                }
                animator.SetBool("IsJump", true);;
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
            }
            else if (Input.GetButtonUp("Down"))
            {
                lookDown = false;
                animator.SetBool("IsLookDown", false);
            }

            // AM 05-02-20 check to see if the animator should play the attack animation
            // JV 05-04-20: Modified attack animation
            if (Input.GetButtonDown("Attack1"))
            {
                if(curr_time > 0.0f)
                {
                    animator.SetBool("IsAttack", true);
                    Debug.Log("ATTACKING");
                    isAttacking = true;
                } else
                {
                    animator.SetBool("IsAttacking", true);
                    isAttacking = false;
                }
            }
            else if(Input.GetButtonUp("Attack1"))
            {
                animator.SetBool("IsAttack", false);
                isAttacking = false;
                curr_time = pickout_timer;
            }

            if (vectorbool == true)
            {
                animator.SetBool("IsFalling", false);
            }
            else
            {
                animator.SetBool("IsJump", false);
                animator.SetBool("IsFalling", true);
            }
        }

        
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if(collisionInfo.gameObject.tag == "Wall" && isAttacking && latch_count > 0)
        {
            // Collided with wall & this player is attacking & have not surpassed permitted latch times
            isLatched = true;
            latch_count -= 1;
            Debug.Log("Colliding with wall");
        }
        if (collisionInfo.gameObject.tag == "Ground")
        {
            // Standing or falling towards ground
            isLatched = false;
            latch_count = latch_times;
            Debug.Log("Colliding with ground");
        }
    }

    void FixedUpdate()
    {
        //Character Movement
        controller.Move(horizontalMove*Time.fixedDeltaTime, jump);
        jump = false;

        if(curr_time > 0.0f)
        {
            curr_time -= 1f * Time.fixedDeltaTime;
        }
    }
    
    // JV 05-04-2020: Added for crate break functionality
    public bool CheckAttack()
    {
        if (isAttacking)
            return true;
        else
            return false;
    }
}
