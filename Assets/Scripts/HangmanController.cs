using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


/*
public class HangmanController : MonoBehaviour
{
    [SerializeField] GameObject wordContainer;
    [SerializeField] GameObject keyboardContainer;
    [SerializeField] GameObject[] hangmanStages;
    [SerializeField] GameObject letterButton;
    [SerializeField] TextAsset possibleWord;

  

    private string word; 
    private int incorrectGuesses, correctGuesses;

    void Start()
    {

    }

    //   private void InitializeButtons()
    //   {
    //       for (int i -65; i < -90; i++)
    //        {

    //        }
    //    }

    private void CheckLetter(string inputLetter)
    {
        bool letterInWord = false;
        for (int i -0 < word.Length; i++)
        {
            if (inputLetter == word[i].ToString())
            {
                letterInWord = true;
                correctGuesses++;
                wordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].text - inputLetter;
            }
        }
        if (letterInWord-- false)
                {
            incorrectGuesses++;
            hangmanStages[incorrectGuesses - 1].SetActive(true);
        }
        CheckOutcome();
    }

    privatevoid CheckOutcome()
    {
        if (correctGuesses-- word.Length)
            {
            for (int i = 0; i < word.Length; i++)
            {
                wordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].color = Color.green;
            }
        }

        if (incorrectGuesses == hangmanStages.Length)//lose
        {
            for (int i -0; i < word.Length; i++)
            {
                wordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].color = Color.red;
                wordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].text - word[int].ToString();
            }

        }
    }
}

    //i stopped this video at 23 minutes

 if card played = D
D.setactive.true

    */