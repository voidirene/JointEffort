using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private Transform pauseModal;
    [SerializeField] private UnityEvent pauseGame;
    [SerializeField] private UnityEvent unpauseGame;
    private bool isPaused;

    private Transform optionsObject;

    private void Awake()
    {
        // if in a level, get the pause modal
        if (SceneManager.GetActiveScene().name.Contains("Level"))
        {
            pauseModal = GameObject.Find("PauseModal").transform;
        }
        else 
            Debug.Log("No pause modal found. You may not be in a level or the modal object is missing.");

        // if in a level, get the pause modal
        if (SceneManager.GetActiveScene().name.Contains("Menu"))
        {
            optionsObject = GameObject.Find("OptionsScreen").transform;
        }
        else 
            Debug.Log("No options gameobject found. You may not be in the main menu or the options object is missing.");
    }

    private void Start()
    {
        if (pauseModal != null)
            pauseModal.gameObject.SetActive(false);
        if (optionsObject != null)
            optionsObject.gameObject.SetActive(false);
    }

    private void Update()
    {
        //pausing method
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name.Contains("Level"))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else if (isPaused)
            {
                UnpauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;

        Time.timeScale = 0;
        pauseGame.Invoke();
    }
    public void UnpauseGame()
    {
        isPaused = false;

        Time.timeScale = 1;
        unpauseGame.Invoke();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
