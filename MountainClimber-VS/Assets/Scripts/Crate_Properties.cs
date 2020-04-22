using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate_Properties : MonoBehaviour
{
    public Animator animator;
    public BoxCollider2D box_collider;
    public Rigidbody2D rb;

    bool collided = false;
    public float secondsTilDeath = 0.5f;

    void Start()
    {
        animator.enabled = false;
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        Debug.Log("COLLISION WITH CRATE");
        if (collisionInfo.gameObject.tag == "Player")
        {
            collided = true;
            //movement.enabled = false;
        }
    }

    void Update()
    {
        if(collided)
        {
            animator.enabled = true;
            animator.Play("Crate_Break");
            box_collider.enabled = false;
            Destroy(rb);
            Destroy(gameObject, secondsTilDeath);
        }
    }
}
