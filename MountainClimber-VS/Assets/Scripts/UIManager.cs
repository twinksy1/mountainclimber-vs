using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // AM - 04-26-20 Reload will reload the current level that the player is at.
        // sceneToLoad will be the name of the scene that we want to load
        // This name will be added in the unity editor. It should be "ScollingTest"
    public void Reload(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
