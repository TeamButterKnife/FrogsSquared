using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GlobalTimekeeper gameClock;
    [SerializeField] private float gameTimeLimit;
    [SerializeField] private int currentLevel;
    void Awake()
    {
        gameClock.SetStartTime();
        gameClock.GameTimeLimit = gameTimeLimit;
        currentLevel = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(currentLevel, LoadSceneMode.Additive);
    }

    public void GoNextLevel()
    {
        if (currentLevel < SceneManager.sceneCountInBuildSettings + 1)
        {
            currentLevel++;
            SceneManager.LoadSceneAsync(currentLevel, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(currentLevel - 1);
        }
        else return;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
