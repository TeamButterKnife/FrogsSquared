using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSky : MonoBehaviour
{   
    private float startTime;
    private Camera cam;
    private bool doneSwitch = false;
    [SerializeField] Color startColor;
    [SerializeField] Color midColor;
    [SerializeField] Color endColor;
    [SerializeField] int colorSwitchPercentage;
    [SerializeField] float gameTimeLimit = 300f;
    [SerializeField] private Color lerpColor1, lerpColor2;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.fixedTime;
        cam = transform.GetComponent<Camera>();
        lerpColor1 = startColor;
        lerpColor2 = midColor;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float secondsElapsed = (Time.fixedTime - startTime);
        float skyPercentage = (secondsElapsed / gameTimeLimit);
        // skyPercentage /= 100f;
        Debug.Log(secondsElapsed);
        if(!doneSwitch)
        {
            if(skyPercentage > (colorSwitchPercentage / 100f))
            {
                lerpColor1 = cam.backgroundColor;
                lerpColor2 = endColor;
                gameTimeLimit -= secondsElapsed;
                doneSwitch = true;
                skyPercentage = 0f;
                startTime = Time.fixedTime;
            }
        }
        cam.backgroundColor = Color.Lerp(lerpColor1, lerpColor2, skyPercentage);
    }
}
