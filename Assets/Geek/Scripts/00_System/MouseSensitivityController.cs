using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSensitivityController : MonoBehaviour
{
    public static MouseSensitivityController instance;

    public float mouseSense;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
}
