using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class LightFlickerScript : MonoBehaviour
{
    private Light2D newlight;

    void Start()
    {
        newlight = GetComponent<Light2D>();

        InvokeRepeating("Flicker", 0f, 0.1f);
    }
    // Update is called once per frame
    void Flicker()
    {
        newlight.intensity = Random.Range(0.46f,0.49f);
    }
}
