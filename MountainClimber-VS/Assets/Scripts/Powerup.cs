// Maintained by: Juan Villasenor
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
    private bool super_jump;
    private float duration;
    public float jump_multiplier = 1.5f;
    // Give bonus points
    private bool recieveBonus;

    void Start()
    {
        enemy_speedup = false;
        cam_slowdown = false;
        super_jump = false;
        recieveBonus = false;
        duration = 0.0f;
    }

    void Update()
    {
        // Player only has extra high jumping for 10 secs
        if(duration > 0.0f)
        {
            duration -= 1.0f * Time.deltaTime;
        } else
        {
            if(duration != 0.0f)
            {
                duration = 0.0f;
                GetComponent<CharacterController2D>().m_JumpForce /= jump_multiplier;
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
        super_jump = true;
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
    public bool CheckSuperJump()
    {
        if(super_jump == true)
        {
            super_jump = false;
            duration = 10.0f;
            GetComponent<CharacterController2D>().m_JumpForce *= jump_multiplier;
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
