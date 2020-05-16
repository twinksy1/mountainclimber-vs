// Maintained by: Juan Villasenor
// JV: 05-11-2020: Creation of script
// What power up objects do
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupCollision : MonoBehaviour
{
    public Rigidbody2D rb;
    public float MaxXForce = 100.0f;
    public float MaxYForce = 100.0f;
    private bool collided = false;

    void Start()
    {
        // Launch object out of the crate
        float XForce = Random.Range(-MaxXForce, MaxXForce);
        float YForce = Random.Range(MaxYForce/2, MaxYForce);
        Vector2 force = new Vector2(XForce * Time.deltaTime, YForce * Time.deltaTime);
        rb.AddForce(force);
        collided = false;
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collided) return;
        if(collisionInfo.gameObject.tag == "PlayerOne" || collisionInfo.gameObject.tag == "PlayerTwo")
        {
            // Collided with player
            switch(this.tag)
            {
                case "JumpPowerup":
                    collisionInfo.gameObject.GetComponent<Powerup>().SetSuperJump();
                    collisionInfo.gameObject.GetComponent<Powerup>().SetBonusPoints();
                    break;
                case "CamSlowPowerup":
                    collisionInfo.gameObject.GetComponent<Powerup>().SetCamSlowdown();
                    collisionInfo.gameObject.GetComponent<Powerup>().SetBonusPoints();
                    break;
                case "CamSpeedupPowerup":
                    collisionInfo.gameObject.GetComponent<Powerup>().SetEnemySpeedup();
                    collisionInfo.gameObject.GetComponent<Powerup>().SetBonusPoints();
                    break;
            }

            collided = true;
        }
    }

    void Update()
    {
        if (collided)
        {
            Destroy(gameObject);
        }
    }
}
