using UnityEngine;

// JV 04-28-2020: Modified code so its applicable to all camera objects

public class scroll : MonoBehaviour
{
    public Transform t;
    public float speed = .1f;

    // Update is called once per frame
    void Update()
    {
        var cameraPosition = t.position;
        cameraPosition.y += speed;
        t.position = cameraPosition;
    }
}
