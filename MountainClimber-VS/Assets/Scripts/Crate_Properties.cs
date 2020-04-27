using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Crate_Properties : MonoBehaviour
{
    // Components
    public Animator animator;
    public Transform t;
    public BoxCollider2D box_collider;
    public Rigidbody2D rb;
    public TextMeshProUGUI bonus_points;

    // Check if player collided with this crate
    bool collided = false;
    private float offset = 1.7f;
    // Kill crate object after collision
    public float secondsTilDeath = 0.5f;

    void Start()
    {
        // Disable continuous animation
        animator.enabled = false;
        bonus_points.enabled = false;
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        // Collision detection
        if (collisionInfo.gameObject.tag == "Player" && //collisionInfo.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0.0f &&
            collisionInfo.gameObject.GetComponent<Transform>().position.y > t.position.y+offset)
        {
            Debug.Log("COLLISION WITH CRATE");
            // Call function in player movement script
            collisionInfo.gameObject.GetComponent<PlayerMovement>().setBonusPoints();
            collided = true;
        }
    }

    void Update()
    {
        if(collided)
        {
            // Play the crate break animation and destroy the crate object
            animator.enabled = true;
            bonus_points.enabled = true;
            animator.Play("Crate_Break");
            box_collider.enabled = false;
            Destroy(rb);
            Destroy(gameObject, secondsTilDeath);
        }
    }
}
