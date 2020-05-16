using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Toggle uniqueToggle;
    public bool uniqueOn;
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void OnEnable()
    {
        uniqueToggle.onValueChanged.AddListener(delegate { OnUniqueToggle(); });
    }
    
    public void OnUniqueToggle()
    {
        uniqueOn = uniqueToggle.isOn;
    }
}
