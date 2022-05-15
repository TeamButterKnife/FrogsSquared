using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GlobalTimekeeper gameClock;
    public DateTime GetStartTime() => gameClock.GameStartTime;
    public float GetTimeLimit() => gameClock.GameTimeLimit;
    private void Awake()
    {
        if(gameClock.GameStartTime.Year < 2022)
        {
            gameClock.SetStartTime();
        }
        Debug.Log(gameClock.GameStartTime);
    }
}
