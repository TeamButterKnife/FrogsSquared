using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GlobalTimekeeper gameClock;
    public DateTime GetStartTime() => gameClock.GameStartTime;
    public float GetTimeLimit() => gameClock.GameTimeLimit;
    private void Awake()
    {
        Debug.Log(gameClock.GameStartTime);
    }
}
