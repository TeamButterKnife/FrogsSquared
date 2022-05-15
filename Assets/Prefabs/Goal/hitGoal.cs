using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hitGoal : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Frog")
        {
            Debug.Log("You win!");
            // Need to change to the "next" scene. Don't have that next part set up yet.
            int levelIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(levelIndex + 1);
        }
    }
}
