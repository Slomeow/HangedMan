using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool hasbeenPlayed;

    public int handIndex;

    public string cardLetter;

    private SpriteRenderer sr;
    private Color originalColor;

    private GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        sr = GetComponent<SpriteRenderer>(); // Assign the global sr
        originalColor = sr.color;

        // Use sr to get the sprite's name
        if (sr != null && sr.sprite != null)
        {
            cardLetter = sr.sprite.name.ToUpper();
        }
        else
        {
            Debug.LogWarning("Card sprite or SpriteRenderer not found!");
        }
    }

    private void OnMouseDown()
    {
        if (!hasbeenPlayed)
        {
            hasbeenPlayed = true;
            gm.availableCardSlots[handIndex] = true;

            // Move up slightly
            transform.position += Vector3.up * 1.5f;
            //transform.position += Vector3.up * 0.5f;
            StartCoroutine(ShakeCard());

            bool isCorrect = gm.CheckLetter(cardLetter);

            // Remove from activeCards list
            gm.activeCards.Remove(this);

            // Handle feedback and discard
            StartCoroutine(HandleCardResult(isCorrect));

            // Return the rest of the cards
            gm.ReturnUnplayedCards(this);
        }
    }

    private IEnumerator HandleCardResult(bool isCorrect)
    {
        if (isCorrect)
        {
            yield return StartCoroutine(FadeToGold());
            yield return new WaitForSeconds(0.3f);
        }
        else
        {
            gm.AddIncorrectLetter(cardLetter);
            gm.LoseLife();
            yield return StartCoroutine(FadeToBlack());
            yield return new WaitForSeconds(0.3f);
        }

        gm.ReportLetterGuess(cardLetter, isCorrect);
        gm.availableCardSlots[handIndex] = true;
        gm.discardPile.Add(this);
        gameObject.SetActive(false);
    }

    private IEnumerator ShakeCard(float duration = 0.3f, float magnitude = 0.04f)
    {
        Vector3 originalPos = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos;
    }

    private IEnumerator FadeToBlack(float duration = 0.3f)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            sr.color = Color.Lerp(originalColor, Color.black, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        sr.color = Color.black;
    }

    private IEnumerator FadeToGold(float duration = 0.3f)
    {
        Color goldColor = new Color(1f, 0.84f, 0.2f); // a nice golden yellow
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            sr.color = Color.Lerp(originalColor, goldColor, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        sr.color = goldColor;
    }
}

//public void ResetHangman()
//{
  //  lives = 6;

  //  foreach (GameObject part in hangmanParts)
  //  {
    //    part.SetActive(false);
   // }

  //  ResetIncorrectLetters();
//}

//public void ResetIncorrectLetters() call this method when restarting the game
//{
//   incorrectLetters.Clear();
//   UpdateIncorrectLettersUI();
//}

//void MoveToDiscardPile()
// {
//   gm.discardPile.Add(this);
//    gameObject.SetActive(false);
//   }
//}