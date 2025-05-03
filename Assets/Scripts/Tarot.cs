using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tarot : MonoBehaviour
{
    public bool ThasbeenPlayed;
    private SpriteRenderer sr;
    private Color originalColor;
    public Color highlightColor = new Color(1f, 0.95f, 0.75f); // Gold

    public int ThandIndex;

    public string tarotName; // Assign in Inspector or via script

    private GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        tarotName = gameObject.name;
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    private void OnMouseDown()
    {
        if (!ThasbeenPlayed && !gm.hasUsedTarotThisRound)
        {
            ThasbeenPlayed = true;
            gm.hasUsedTarotThisRound = true; // Mark Tarot as used this round
            StartCoroutine(UseTarotCard());
        }
    }

    IEnumerator UseTarotCard()
    {
        transform.position += Vector3.up * 3;
        Vector3 originalPos = transform.position;
        float shakeDuration = 0.2f;
        float shakeAmount = 0.2f;

        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            transform.position = originalPos + (Vector3)Random.insideUnitCircle * shakeAmount;
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPos;

        float duration = 0.5f;
        float t = 0f;

        // Fade to gold
        while (t < duration)
        {
            sr.color = Color.Lerp(originalColor, highlightColor, t / duration);
            t += Time.deltaTime;
            yield return null;
        }
        sr.color = highlightColor;

        // Fade to transparent
        t = 0f;
        Color gold = highlightColor;
        Color transparent = new Color(gold.r, gold.g, gold.b, 0f);
        while (t < duration)
        {
            sr.color = Color.Lerp(gold, transparent, t / duration);
            t += Time.deltaTime;
            yield return null;
        }
        sr.color = transparent;
        // Optional: Play animation or effect here

        TriggerTarotEffect();
        yield return new WaitForSeconds(1f);

        gm.availableTarotSlots[ThandIndex] = true;
        gm.TdiscardPile.Add(this);
        gameObject.SetActive(false);

        // You can now allow a new Tarot card to be drawn if needed
    }

    void TriggerTarotEffect()
    {
        switch (tarotName)
        {
            case "The Emperor":
                gm.DrawExtraLetterToFourthSlot();
                break;
            // Add more cases here
            case "Strength":
                
                    gm.GainLife();
                break;

            case "The World":

                   gm.TheWorldCard();
                break;

            case "The Moon":

                gm.TheMoonCard();
                break;

            case "Death":
                gm.UseDeathTarotCard();
                break;

            default:
                Debug.Log("No effect for " + tarotName);
                break;
        }
    }
    // void TMoveToDiscardPile()
    //  {
    //       gm.TdiscardPile.Add(this);
    //     gameObject.SetActive(false);
    //  }
}