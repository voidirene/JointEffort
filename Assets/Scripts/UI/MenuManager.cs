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

    private Transform winModal;
    private bool isBlueReady = false;
    private bool isRedReady = false;
    [SerializeField] private UnityEvent winGame;

    [SerializeField] private UnityEvent loseGame;

    private Transform optionsObject;

    private void Awake()
    {
        // if in a level, get the pause modal
        if (SceneManager.GetActiveScene().name.Contains("Level"))
        {
            pauseModal = GameObject.Find("PauseModal").transform;
            winModal = GameObject.Find("WinModal").transform;
        }
        else
            Debug.Log("No pause/win modal found. You may not be in a level or the modal object(s) is/are missing.");

        // if in a level, get the pause modal and show player IDs
        if (SceneManager.GetActiveScene().name.Contains("Menu"))
        {
            optionsObject = GameObject.Find("OptionsScreen").transform;

            GameObject.Find("ResearchScreen").GetComponent<SurveyIDGenerator>().ShowID();
        }
        else
            Debug.Log("No options gameobject found. You may not be in the main menu or the options object is missing.");
    }

    private void Start()
    {
        if (pauseModal != null)
            pauseModal.gameObject.SetActive(false);
        if (winModal != null)
            winModal.gameObject.SetActive(false);
        if (optionsObject != null)
            optionsObject.gameObject.SetActive(false);

        Time.timeScale = 1;
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

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetBlueReadyStatus(bool isReady)
    {
        isBlueReady = isReady;
        GameWin();
    }
    public void SetRedReadyStatus(bool isReady)
    {
        isRedReady = isReady;
        GameWin();
    }

    public void GameWin()
    {
        if (isBlueReady && isRedReady)
        {
            winGame.Invoke();
            Time.timeScale = 0;
        }
    }

    public void GameLose()
    {
        loseGame.Invoke();
        StartCoroutine(TriggerPlayerDeath());
    }
    private IEnumerator TriggerPlayerDeath()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            player.GetComponent<AnimationController>().Explode();
        }

        yield return new WaitForSeconds(0.5f);
        RestartLevel();
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
