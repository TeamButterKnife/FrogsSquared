using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ScriptieBoi", order = 1)]
public class ScriptieBoi : ScriptableObject
{
    public string prefabName;
    public int iHoldStuff;

    public int numberOfPrefabsToCreate;
    public Vector3[] spawnPoints;

    public void AddPoint()
    {
        iHoldStuff += 1;
    }
}