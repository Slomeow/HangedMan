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

    public List<Tarot> Tdeck = new List<Tarot>();
    public List<Tarot> TdiscardPile = new List<Tarot>();

    public List<Card> activeCards = new List<Card>();

    public List<GameObject> hangmanParts;
    private int lives = 6;

    public Transform[] cardSlots;
    public Transform[] tarotSlots;
    public bool[] availableCardSlots;
    public bool[] availableTarotSlots;

    public bool hasUsedTarotThisRound = false;

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

    public bool HasTarotInHand()
    {
        foreach (bool slotTaken in availableTarotSlots)
        {
            if (!slotTaken) return true;
        }
        return false;
    }
//
    public void OnLetterCardPlayed()
    {
        hasUsedTarotThisRound = false;
    }
//
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

        activeCards.Clear();
    }

    public void DrawCard()
    {

        if (deck.Count >= 1)
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

    public void DrawTarot()
    {
        if (Tdeck.Count >= 1 && !HasTarotInHand())
        {
            Tarot randTarot = Tdeck[Random.Range(0, Tdeck.Count)];

            for (int i = 0; i < availableTarotSlots.Length; i++)
            {
                if (availableTarotSlots[i] == true)
                {
                    randTarot.gameObject.SetActive(true);
                    randTarot.ThandIndex = i;

                    randTarot.transform.position = tarotSlots[i].position;
                    randTarot.ThasbeenPlayed = false;

                    availableTarotSlots[i] = false;
                    Tdeck.Remove(randTarot);
                    return;
                }
            }
        }
    }

    public void Shuffle()
    {
        if (discardPile.Count >= 1)
        {
            foreach (Card card in discardPile)
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

    public void GainLife()
    {
        if (lives < 6)
        {
            lives++;

            int partIndex = 6 - lives;
            if (partIndex >= 0 && partIndex < hangmanParts.Count)
            {
                hangmanParts[partIndex].SetActive(true);
            }
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

    public void DrawExtraLetterToFourthSlot()
    {
        Debug.Log("Attempting to draw a card to 4th slot...");
        availableCardSlots[3] = true; // forcibly enable slot 4

        if (deck.Count < 1)
        {
            Debug.LogWarning("No cards left in the deck!");
            return;
        }

        if (availableCardSlots.Length < 4)
        {
            Debug.LogWarning("Card slots array not large enough!");
            return;
        }

        if (!availableCardSlots[3])
        {
            Debug.LogWarning("4th card slot is not available!");
            return;
        }

        Card randCard = deck[Random.Range(0, deck.Count)];
        randCard.gameObject.SetActive(true);
        randCard.handIndex = 3;

        randCard.transform.position = cardSlots[3].position;
        randCard.hasbeenPlayed = false;

        availableCardSlots[3] = false;
        deck.Remove(randCard);

        Debug.Log("Card drawn into 4th slot!");
    }

    public void TheWorldCard()
    {
        List<int> unrevealedIndices = new List<int>();

        for (int i = 0; i < targetWord.Length; i++)
        {
            if (revealedLetters[i] == '_')
            {
                unrevealedIndices.Add(i);
            }
        }

        if (unrevealedIndices.Count > 0)
        {
            int randomIndex = unrevealedIndices[Random.Range(0, unrevealedIndices.Count)];
            revealedLetters[randomIndex] = targetWord[randomIndex];
            UpdateWordDisplay();
        }
    }

    public void TheMoonCard()
    {
        List<Card> incorrectCards = new List<Card>();

        foreach (Card card in FindObjectsOfType<Card>())
        {
            if (card.gameObject.activeInHierarchy && !string.IsNullOrEmpty(card.cardLetter))
            {
                if (!targetWord.Contains(card.cardLetter.ToUpper()))
                {
                    incorrectCards.Add(card);
                }
            }
        }

        if (incorrectCards.Count > 0)
        {
            Card cardToRemove = incorrectCards[Random.Range(0, incorrectCards.Count)];
            StartCoroutine(RemoveCardWithEffect(cardToRemove));
        }
        else
        {
            Debug.Log("no incorrect cards to remove");
        }
    }

    private IEnumerator RemoveCardWithEffect(Card card)
    {
        SpriteRenderer sr = card.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Color originalColor = sr.color;
            sr.color = Color.red; // Moon effect color
            yield return new WaitForSeconds(0.5f);
            sr.color = originalColor;
        }

        availableCardSlots[card.handIndex] = true;
        discardPile.Add(card);
        card.gameObject.SetActive(false);
    }

    public void UseDeathTarotCard()
    {
        Debug.Log("Death card used: Reshuffling current letter hand.");

        List<Card> activeHandCards = new List<Card>();

        // Loop through only the card slots used for hand
        for (int i = 0; i < availableCardSlots.Length; i++)
        {
            if (!availableCardSlots[i]) // slot is occupied
            {
                foreach (Card card in FindObjectsOfType<Card>())
                {
                    if (card.handIndex == i && card.gameObject.activeInHierarchy)
                    {
                        activeHandCards.Add(card);
                        break;
                    }
                }
            }
        }

        // Return those cards to the deck and clear the slots
        foreach (Card card in activeHandCards)
        {
            availableCardSlots[card.handIndex] = true;
            card.hasbeenPlayed = false;
            card.gameObject.SetActive(false);
            deck.Add(card);
        }

        // Disable 4th slot (from The Emperor) if it's available
        if (availableCardSlots.Length >= 4)
        {
            availableCardSlots[3] = false;
        }

        // Draw 3 new cards
        for (int i = 0; i < 3; i++)
        {
            DrawCard();
        }
    }

    public void DiscardRemainingLetterCards()
    {
        foreach (Card card in FindObjectsOfType<Card>())
        {
            if (!card.hasbeenPlayed)
            {
                availableCardSlots[card.handIndex] = true;
                discardPile.Add(card);
                card.gameObject.SetActive(false);
            }
        }

        // Reset slot 4 (index 3) if it was used
        if (availableCardSlots.Length >= 4)
        {
            availableCardSlots[3] = false;
        }
    }
    public void ResetFourthSlot()
    {
        if (availableCardSlots.Length >= 4)
        {
            availableCardSlots[3] = false;
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
