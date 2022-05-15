using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GlobalTimekeeper", order = 1)]
public class GlobalTimekeeper : ScriptableObject 
{
    private DateTime gameStartTime;
    private float gameTimeLimit;
    public DateTime GameStartTime { get => gameStartTime; }
    public float GameTimeLimit { get => gameTimeLimit; set => gameTimeLimit = value; }

    public void SetStartTime()
    {
        gameStartTime = DateTime.Now;
    }
}