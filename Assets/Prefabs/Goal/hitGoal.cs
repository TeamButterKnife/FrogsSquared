using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hitGoal : MonoBehaviour
{
    [SerializeField] public string nextSceneName;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Frog")
        {
            Debug.Log("You win!");
            // Need to change to the "next" scene. Don't have that next part set up yet.
            // int levelIndex = SceneManager.GetActiveScene().buildIndex;
            Scene currentLevel = SceneManager.GetActiveScene();
            other.enabled = false;
            // Debug.Log(levelIndex);
            // SceneManager.LoadScene(levelIndex + 1, LoadSceneMode.Additive);
            // SceneManager.UnloadSceneAsync(levelIndex - 1, UnloadSceneOptions.None);

            // SceneManager.LoadScene(nextSceneName, LoadSceneMode.Additive);
            // SceneManager.UnloadSceneAsync(currentLevel.buildIndex - 1);
            FindObjectOfType<GameManager>().GoNextLevel();
        }
    }
}
