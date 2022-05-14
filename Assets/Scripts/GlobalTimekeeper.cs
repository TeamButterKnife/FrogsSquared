using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GlobalTimekeeper", order = 1)]
public class GlobalTimekeeper : ScriptableObject 
{
    private DateTime gameStartTime;
    public DateTime GameStartTime { get => gameStartTime; }

    public void SetStartTime()
    {
        gameStartTime = DateTime.Now;
    }
}