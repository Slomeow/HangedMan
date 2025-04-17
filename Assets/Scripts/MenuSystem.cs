using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class StartMenu : MonoBehaviour
{
    public List<Button> menuButtons;
    private int selectedIndex = 0;

    void Start()
    {
        {
            if (menuButtons.Count == 0)
            {
                Debug.LogError("Menu buttons list is empty! Assign buttons in the Inspector.");
                return;
            }
            HighlightButton();
        }
    }

    void Update()
    {
        HandleKeyboardInput();
    }

    void HandleKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedIndex = (selectedIndex + 1) % menuButtons.Count;
            HighlightButton();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedIndex = (selectedIndex - 1 + menuButtons.Count) % menuButtons.Count;
            HighlightButton();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            menuButtons[selectedIndex].onClick.Invoke();
        }
    }

    void HighlightButton()
    {
       
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Card Game");
    }

    public void OpenOptions()
    {
        SceneManager.LoadScene("Rules");
        Debug.Log("Options Menu Opened");
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}