using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3ManagerScript : MonoBehaviour
{
    Light mainLight;

    private void Awake()
    {
        mainLight = FindObjectOfType<Light>();    
    }

    private void Start()
    {
        mainLight.enabled = false;
    }
}
