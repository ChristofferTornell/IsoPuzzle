using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightSourceInteractable : MonoBehaviour
{
    public Light2D light2D;
    private bool turningOn = false;
    private bool turningOff = false;
    [HideInInspector] public bool lightActive = true;
    private float LightIntensity = 1f;
    public SpriteMask spriteMask;
    public float lightIntensity
    {
        get { return LightIntensity; }
        set 
        { 
            LightIntensity = value;
            light2D.intensity = LightIntensity;
            if (light2D.intensity <= 0 && lightActive)
            {
                lightActive = false;
                spriteMask.gameObject.SetActive(false);
            }
            else if (light2D.intensity > 0 && !lightActive)
            {
                lightActive = true;
                spriteMask.gameObject.SetActive(true);

            }
        }
    }

    public void TurnOff(float fadeSpeed)
    {
        if (!turningOff) { StartCoroutine(TurnOffCo(fadeSpeed)); }
    }
    public void TurnOn(float fadeSpeed)
    {
        if (!turningOn) { StartCoroutine(TurnOnCo(fadeSpeed)); }

    }
    private IEnumerator TurnOffCo(float fadeSpeed)
    {
        turningOn = false;
        turningOff = true;
        while (turningOff && !turningOn && lightIntensity > 0)
        {
            lightIntensity -= fadeSpeed/100;
            yield return null;
        }
        if (lightIntensity < 0) { lightIntensity = 0; }
        turningOff = false;
        yield return null;
    }
    private IEnumerator TurnOnCo(float fadeSpeed)
    {
        turningOn = true;
        turningOff = false;
        while (!turningOff && turningOn && lightIntensity <= 1)
        {
            lightIntensity += fadeSpeed/100;
            yield return null;
        }
        turningOn = false;
        yield return null;
    }
}
