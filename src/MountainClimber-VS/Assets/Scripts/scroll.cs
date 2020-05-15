// JV 05-11-2020: Added speedup animation
// JV 04-28-2020: Modified code so its applicable to all camera objects
using UnityEngine;
using TMPro;

public class scroll : MonoBehaviour
{
    public Transform t;
    public float speed = .01f, inc = .01f;
    public int currentTime, interval = 13;

    public TextMeshProUGUI speedup;
    public Animator anim;
    public int cam;

    void Start()
    {
        anim = speedup.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = (int)Time.timeSinceLevelLoad;
        if (currentTime > interval)
        {
            speed += inc;
            interval += 10;
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