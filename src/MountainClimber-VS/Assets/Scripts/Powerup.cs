// Maintained by: Juan Villasenor
// JV: 05-11-2020: Fixed super jump remaining activated glitch
// JV: 05-10-2020: Creation of script
// Script to handle power ups
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    // Speedup rival's cam
    private bool enemy_speedup;
    // Slow down this player's cam
    private bool cam_slowdown;
    // Give this player a jump boost
    public float duration = 10.0f;
    private bool super_jump;
    private float curr_time;
    public float jump_multiplier = 1.5f;
    // Give bonus points
    private bool recieveBonus;

    void Start()
    {
        enemy_speedup = false;
        cam_slowdown = false;
        super_jump = false;
        recieveBonus = false;
        curr_time = duration;
    }

    void Update()
    {
        // Player only has extra high jumping for 10 secs
        if(super_jump)
        {
            if(curr_time > 0.0f)
            {
                curr_time -= 1.0f * Time.deltaTime;
            }

            else
            {
                duration = 0.0f;
                GetComponent<CharacterController2D>().m_JumpForce /= jump_multiplier;
                super_jump = false;
            }
            
        } 
    }
    // Mutators
    public void SetEnemySpeedup()
    {
        enemy_speedup = true;
    }
    public void SetCamSlowdown()
    {
        cam_slowdown = true;
    }
    public void SetSuperJump()
    {
        if(!super_jump)
        {
            super_jump = true;
            curr_time = duration;
            GetComponent<CharacterController2D>().m_JumpForce *= jump_multiplier;
        }
    }
    public void SetBonusPoints()
    {
        recieveBonus = true;
    }
    // Accessors
    public bool CheckEnemySpeedup()
    {
        if(enemy_speedup == true)
        {
            enemy_speedup = false;
            return true;
        } else
        {
            return false;
        }
    }
    public bool CheckCamSlowdown()
    {
        if (cam_slowdown == true)
        {
            cam_slowdown = false;
            return true;
        } else
        {
            return false;
        }
    }
    public bool CheckBonus()
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
