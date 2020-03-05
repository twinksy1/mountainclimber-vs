using UnityEngine;

public class scroll : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = .1f;

    // Update is called once per frame
    void Update()
    {
        var cameraPosition = Camera.main.gameObject.transform.position;
        cameraPosition.y += speed;
        Camera.main.gameObject.transform.position = cameraPosition;
    }
}
