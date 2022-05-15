using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hitGoal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Frog")
        {
            Debug.Log("You win!");
            Scene currentLevel = SceneManager.GetActiveScene();
            other.enabled = false;
            FindObjectOfType<GameManager>().GoNextLevel();
        }
    }
}
