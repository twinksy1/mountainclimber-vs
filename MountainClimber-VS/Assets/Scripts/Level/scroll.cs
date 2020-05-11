using UnityEngine;
using TMPro;

// JV 04-28-2020: Modified code so its applicable to all camera objects
// JV 05-08-2020: Added speedup animation play on speedup

public class scroll : MonoBehaviour
{
    public Transform t;
    private float starting_speed = 0.01f;
    public float speed = .1f;
    public float inc = .01f;
    public int currentTime, interval = 13;

    // Speedup animation
    public TextMeshProUGUI speedup;
    private Animator anim;
    public int cam;

    void Start()
    {
        speed = starting_speed;
        speedup.GetComponent<Animator>().enabled = false;
        anim = speedup.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = (int)Time.time;
        if (currentTime > interval)
        {
            speed += inc;
            interval += interval;
            anim.enabled = true;
            if(cam == 1)
            {
                anim.Play("Speedup", -1, 0);
            } else
            {
                anim.Play("Speedup2", -1, 0);
            }
        }
        var cameraPosition = t.position;
        cameraPosition.y += speed;
        t.position = cameraPosition;
    }
}
