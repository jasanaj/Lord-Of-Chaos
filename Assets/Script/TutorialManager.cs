using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel;
    public Text tutorialText;
    public Button continueButton;

    private Queue<string> tutorialMessages = new Queue<string>();
    private bool isPaused = false;

    void Start()
    {
        tutorialPanel.SetActive(false);
        continueButton.onClick.AddListener(ContinueTutorial);
    }

    public void ShowTutorial(params string[] messages)
    {
        foreach (string msg in messages)
        {
            tutorialMessages.Enqueue(msg);
        }

        PauseGame();
        DisplayNextMessage();
    }

    void DisplayNextMessage()
    {
        if (tutorialMessages.Count > 0)
        {
            string message = tutorialMessages.Dequeue();
            tutorialText.text = message;
            tutorialPanel.SetActive(true);
        }
        else
        {
            ResumeGame();
        }
    }

    void ContinueTutorial()
    {
        DisplayNextMessage();
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
        tutorialPanel.SetActive(false);
        isPaused = false;
    }

    public bool IsPaused()
    {
        return isPaused;
    }
}
