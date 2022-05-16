using System;
using UnityEngine;

public class MagicSky : MonoBehaviour
{   
    private DateTime startTime;
    private Camera cam;
    private bool doneSwitch = false;
    [SerializeField] Color startColor;
    [SerializeField] Color midColor;
    [SerializeField] Color endColor;
    [SerializeField] int colorSwitchPercentage;
    float gameTimeLimit;
    [SerializeField] private Color lerpColor1, lerpColor2;
    // Start is called before the first frame update
    void Start()
    {
        LevelManager levelManager = GameObject.FindObjectOfType<LevelManager>();
        startTime = levelManager.GetStartTime();
        gameTimeLimit = levelManager.GetTimeLimit();
        cam = transform.GetComponent<Camera>();
        lerpColor1 = startColor;
        lerpColor2 = midColor;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // unperformant emergency solution to game jam time problem
        if (cam is null)
        {
            cam = transform.GetComponent<Camera>();
        }
        float secondsElapsed = (float)((DateTime.Now - startTime).TotalSeconds);
        float skyPercentage = (secondsElapsed / gameTimeLimit);
        // skyPercentage /= 100f;
        // Debug.Log(secondsElapsed);
        if(!doneSwitch)
        {
            if(skyPercentage > (colorSwitchPercentage / 100f))
            {
                lerpColor1 = cam.backgroundColor;
                lerpColor2 = endColor;
                gameTimeLimit -= secondsElapsed;
                doneSwitch = true;
                skyPercentage = 0f;
                startTime = DateTime.Now;
            }
        }
        cam.backgroundColor = Color.Lerp(lerpColor1, lerpColor2, skyPercentage);
    }
}
