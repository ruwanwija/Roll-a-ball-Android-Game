using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] Transform[] panels;
    float waitTime = 4.0f;
    public bool gameOver;
    public bool levelCompleted;
    
    void Awake()
    {
        Time.timeScale = 1f;
        AudioListener.volume = 1f;
        gameOver = false;
        levelCompleted = false;
    }
    
    void Start()
    {
        ManagePanels(0);
    }

    public void GameOver()
    {
        if (!gameOver)
        {
            StartCoroutine(ShowGameOverScreen());
            gameOver = true;
        }
    }

    IEnumerator ShowGameOverScreen()
    {
        yield return new WaitForSeconds(waitTime);
        ManagePanels(2);
        Time.timeScale = 0f;
    }

    public void LevelComplete()
    {
        if (!levelCompleted)
        {
            StartCoroutine(ShowLevelCompleteScreen());
            levelCompleted = true;
        }
    }

    IEnumerator ShowLevelCompleteScreen()
    {
        yield return new WaitForSeconds(waitTime);
        ManagePanels(3);
        Time.timeScale = 0f;
    }

    public void ManagePanels(int panelIndex)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (i == panelIndex)
            panels[i].gameObject.SetActive(true);
            else
            panels[i].gameObject.SetActive(false);
        }
    }
    
    public void BoostRocketUp(bool boosting)
    {
        if (FindObjectOfType<RocketController>())
        FindObjectOfType<RocketController>().ThrustUp(boosting);
    }

    public void BoostRocketDown(bool boosting)
    {
        if (FindObjectOfType<RocketController>())
        FindObjectOfType<RocketController>().ThrustDown(boosting);
    }
    
    public void RotateLeft(bool rotateLeft)
    {
        if (FindObjectOfType<RocketController>())
        FindObjectOfType<RocketController>().RotateLeft(rotateLeft);
    }
    
    public void RotateRight(bool rotateRight)
    {
        if (FindObjectOfType<RocketController>())
        FindObjectOfType<RocketController>().RotateRight(rotateRight);
    }

    public void PauseGame()
    {
        ManagePanels(1);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        ManagePanels(0);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitToMainMenu ()
    {
        SceneManager.LoadScene(0);
    }
}