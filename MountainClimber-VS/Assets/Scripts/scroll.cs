using UnityEngine;

// JV 04-28-2020: Modified code so its applicable to all camera objects

public class scroll : MonoBehaviour
{
    public Transform t;
    public float speed = .01f, inc = .01f;
    public int currentTime, interval = 13;


    // Update is called once per frame
    void Update()
    {
        currentTime = (int)Time.timeSinceLevelLoad;
        if (currentTime > interval)
        {
            speed += inc;
            interval += 10;
        }
        var cameraPosition = t.position;
        cameraPosition.y += speed;
        t.position = cameraPosition;
    }
}
