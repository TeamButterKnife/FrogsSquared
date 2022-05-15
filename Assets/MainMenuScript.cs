using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] GameObject Canvas;
    [SerializeField] GameSettings gameSettings;
    public int casualTimeLimit, normalTimeLimit, hardTimeLimit;
    // [SerializeField] GameObject CanvasCredits;
    // [SerializeField] GameObject CanvasTips;
    // [SerializeField] GameObject BGimage;
    GraphicRaycaster raycaster;

    PointerEventData clickData;
    List<RaycastResult> clickRaycastResults;

    void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();
        clickData = new PointerEventData(EventSystem.current);
        clickRaycastResults = new List<RaycastResult>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            GetUIComponent();
        }
    }

    void TryNewGame(GameObject uiElement)
    {
        switch(uiElement.name)
        {
            case "Casual":
                gameSettings.GameTimeLimit = casualTimeLimit;
                Debug.Log("Setting time limit:" + casualTimeLimit);
                break;
            case "Normal":
                gameSettings.GameTimeLimit = normalTimeLimit;
                break;
            case "Hard":
                gameSettings.GameTimeLimit = hardTimeLimit;
                break;
            default:
                return;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    void GetUIComponent()
    {
        clickData.position = Mouse.current.position.ReadValue();
        clickRaycastResults.Clear();

        raycaster.Raycast(clickData, clickRaycastResults);

        int levelToLoad = 0;

        foreach (RaycastResult result in clickRaycastResults)
        {
            GameObject uiElement = result.gameObject;
            Debug.Log(uiElement.name);
            TryNewGame(uiElement);
            // if (uiElement.TryGetComponent(out LevelSceneLoader levelSceneLoader))
            //     levelToLoad = levelSceneLoader.GetLevelIndex();
            // if (uiElement.TryGetComponent(out PanelHandler panelHandler))
            //     panelHandler.ClosePanel();
        }

        if (levelToLoad != 0)
        {
            // GetComponent<LevelsIndexer>().StartScene(levelToLoad);
        } 
    }

    
}