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
            if(uiElement.name == "NewGame")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            }
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