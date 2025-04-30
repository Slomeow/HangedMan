using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public List<GameObject> tutorialScreens; // Set these in Inspector
    private int currentScreenIndex = 0;

    void Start()
    {
        ShowScreen(0);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AdvanceTutorial();
        }
    }

    void AdvanceTutorial()
    {
        tutorialScreens[currentScreenIndex].SetActive(false);
        currentScreenIndex++;

        if (currentScreenIndex < tutorialScreens.Count)
        {
            ShowScreen(currentScreenIndex);
        }
        else
        {
            EndTutorial();
        }
    }

    void ShowScreen(int index)
    {
        tutorialScreens[index].SetActive(true);
    }

    void EndTutorial()
    {
        SceneManager.LoadScene("Menu");
       // gameObject.SetActive(false);
    }
}
