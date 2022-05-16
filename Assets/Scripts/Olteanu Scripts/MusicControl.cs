using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour
{
    public static MusicControl instance;

    [Header("Music")]
    public bool musicStarted;
    public int musicToPlay;

    private void Awake()
    {
        instance = this;
    }


    private void LateUpdate()
    {
        if (!musicStarted)
        {
            musicStarted = true;

            AudioManager.instance.PlayBGM(musicToPlay);
        }
    }
}
