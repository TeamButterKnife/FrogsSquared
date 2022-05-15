using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameSettings gameClock;
    public DateTime GetStartTime() => gameClock.GameStartTime;
    public float GetTimeLimit() => gameClock.GameTimeLimit;
    private GameManager gameManager;
    private bool isActiveScene = false;
    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        if(gameClock.GameStartTime.Year < 2022)
        {
            gameClock.SetStartTime();
        }
        Debug.Log(gameClock.GameStartTime);
    }

    private void Update()
    {
        if (!isActiveScene)
        {
            // isActiveScene = SceneManager.GetSceneByBuildIndex(gameManager.CurrentLevel).isLoaded;
            // SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(gameManager.CurrentLevel));
            PlayerInput[] players = GameObject.FindObjectsOfType<PlayerInput>();
            foreach(PlayerInput player in players)
            {
                if(player.gameObject.scene.buildIndex == gameManager.CurrentLevel)
                {
                    player.ActivateInput();
                    isActiveScene = true;
                }
                else
                {
                    player.DeactivateInput();
                }
            }
        }
    }
}
