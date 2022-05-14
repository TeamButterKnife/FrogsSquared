using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GlobalTimekeeper gameClock;
    public DateTime GetStartTime()
    {
        return gameClock.GameStartTime;
    }
    private void Awake()
    {
        Debug.Log(gameClock.GameStartTime);
    }
}
