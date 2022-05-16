using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource[] soundsSFX;
    public AudioSource[] bgm;
   // public AudioSource bgma, levelEndMusic;
    
    
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }

        }
        DontDestroyOnLoad(gameObject);
    }

   
    void Update()
    {
        
    }

    public void PlaySFX(int soundEffect)
    {
        soundsSFX[soundEffect].Stop();
        soundsSFX[soundEffect].pitch = Random.Range(.7f, 1.1f);
        soundsSFX[soundEffect].Play();
    }

    public void PlayLevelVictory()
    {
        StopMusic();
        //levelEndMusic.Play();
    }

    public void PlayBGM(int musicToPlay)
    {
        if (!bgm[musicToPlay].isPlaying)
        {
            StopMusic();

            if (musicToPlay < bgm.Length)
            {
                bgm[musicToPlay].Play();
            }
        }
    }

    public void StopMusic()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }

    public void PlayBossMusic()
    {
        StopMusic();
        bgm[0].Play();
    }

    public void StopBossMusic()
    {
        StopMusic();
        bgm[MusicControl.instance.musicToPlay].Play();
    }
}
