using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform t;
    public Rigidbody2D rb;

    public float sideways_speed = 0.2f;
    public float jump_force = 20.0f;
    public int score;
   // Update is called once per frame
    void Update()
    {
        getPlayerInput();
    }

    void getPlayerInput()
    {
        Vector3 new_pos = t.position;
        score = (int)new_pos.y + 5;
        if(Input.GetKey("a"))
        {
            new_pos.x -= sideways_speed;
            t.position = new_pos;
        }

        if(Input.GetKey("d"))
        {
            new_pos.x += sideways_speed;
            t.position = new_pos;
        }

        if(Input.GetKey("w"))
        {
            if(rb.velocity.magnitude == 0.0)
            {
                Vector2 f = new Vector2(0.0f, jump_force * Time.deltaTime);
                rb.AddForce(f);
            }
        }
    }
}
