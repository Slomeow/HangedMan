using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public List<Card> deck = new List<Card>();
    public List<Card> discardPile = new List<Card>();
<<<<<<< Updated upstream
=======
    public List<Tarot> TdiscardPile = new List<Tarot>();

    public List<Card> activeCards = new List<Card>();

    public List<GameObject> hangmanParts;
    private int lives = 6;

>>>>>>> Stashed changes
    public Transform[] cardSlots;
    public bool[] availableCardSlots;

    public Text deckSizeText;
    public Text discardPileText;

    //public List<string> wordList = new List<string> { "CELESTIAL", "DIVINE", "FORTUNE", "ARCANE" };
    //public string targetWord;

    private HashSet<char> correctGuesses = new HashSet<char>();

    public string targetWord = "CELESTIAL";
    private char[] revealedLetters;
    public Text wordDisplayText; // Assign in Inspector

    public Text incorrectLettersText;
    private List<string> incorrectLetters = new List<string>();

    void Start()
    {
        // Setup hidden letters for hangman word
        revealedLetters = new char[targetWord.Length];
        for (int i = 0; i < revealedLetters.Length; i++)
        {
            revealedLetters[i] = '_';
        }

        UpdateWordDisplay();
    }

    public bool CheckLetter(string letter)
    {
        letter = letter.ToUpper();
        bool found = false;

        for (int i = 0; i < targetWord.Length; i++)
        {
            if (targetWord[i].ToString() == letter)
            {
                revealedLetters[i] = targetWord[i];
                found = true;

                //CheckWinCondition();
            }
        }

        UpdateWordDisplay();
        return found;
    }

    void UpdateWordDisplay()
    {
        if (wordDisplayText != null)
        {
            wordDisplayText.text = string.Join(" ", revealedLetters);
        }
    }

    public void ReturnUnplayedCards(Card selectedCard)
    {
        List<Card> cardsToReturn = new List<Card>();

        foreach (Card card in activeCards)
        {
            if (card != selectedCard && !card.hasbeenPlayed && card.gameObject.activeSelf)
            {
                cardsToReturn.Add(card);
            }
        }

        foreach (Card card in cardsToReturn)
        {
            deck.Add(card);
            card.gameObject.SetActive(false);
            availableCardSlots[card.handIndex] = true;
        }

        // Clear all — next draw will refill this fresh
        activeCards.Clear();
    }

    public void DrawCard()
    {

        if(deck.Count >= 1)
        {
            Card randCard = deck[Random.Range(0, deck.Count)];

            for (int i = 0; i < availableCardSlots.Length; i++)
            {
                if (availableCardSlots[i] == true)
                {
                    randCard.gameObject.SetActive(true);
                    randCard.handIndex = i;

                    randCard.transform.position = cardSlots[i].position;
                    randCard.hasbeenPlayed = false;

                    availableCardSlots[i] = false;
                    deck.Remove(randCard);
                    activeCards.Add(randCard); //track it here
                    return;
                }
            }
        }
    }

    public void Shuffle()
    {
        if(discardPile.Count >= 1)
        {
            foreach(Card card in discardPile)
            {
                deck.Add(card);
            }
            discardPile.Clear();
        }
    }

    public void LoseLife()
    {
        lives--;

        int partIndex = 6 - lives - 1;
        if (partIndex >= 0 && partIndex < hangmanParts.Count)
        {
            hangmanParts[partIndex].SetActive(false);
        }

        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void AddIncorrectLetter(string letter)
    {
        if (!incorrectLetters.Contains(letter))
        {
            incorrectLetters.Add(letter);
            UpdateIncorrectLettersUI();
        }
    }

    private void UpdateIncorrectLettersUI()
    {
        incorrectLettersText.text = string.Join(" ", incorrectLetters);
    }

    public void ReportLetterGuess(string letter, bool isCorrect)
    {
        if (isCorrect)
        {
            char c = letter[0];
            if (!correctGuesses.Contains(c))
            {
                correctGuesses.Add(c);
                CheckWinCondition();
            }
        }
    }
    private void CheckWinCondition()
    {
        HashSet<char> uniqueLetters = new HashSet<char>(targetWord.ToCharArray());

        if (correctGuesses.SetEquals(uniqueLetters))
        {
            Debug.Log("You win!");
            SceneManager.LoadScene("Win");
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over! You've been hanged.");

        SceneManager.LoadScene("Lose");
        // Optional: trigger end screen, disable input, etc.
    }

    // Update is called once per frame
    void Update()
    {
        CheckWinCondition();
        deckSizeText.text = deck.Count.ToString();
        discardPileText.text = discardPile.Count.ToString();
    }
}
