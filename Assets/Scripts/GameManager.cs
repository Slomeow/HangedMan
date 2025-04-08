using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public List<Card> deck = new List<Card>();
    public List<Tarot> Tdeck = new List<Tarot>();
    public List<Card> discardPile = new List<Card>();
    public List<Tarot> TdiscardPile = new List<Tarot>();

    public Transform[] cardSlots;
    public Transform[] tarotSlots;
    public bool[] availableCardSlots;
    public bool[] availableTarotSlots;

    public Text deckSizeText;
    public Text discardPileText;
   // public Text TdiscardPile;
    public Text TdeckSizeText;

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
                    return;
                }
            }
        }
    }

    public void DrawTarot()
    {
        if(Tdeck.Count >= 1)
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
        if(discardPile.Count >= 1)
        {
            foreach(Card card in discardPile)
            {
                deck.Add(card);
            }
            discardPile.Clear();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        deckSizeText.text = deck.Count.ToString();
        discardPileText.text = discardPile.Count.ToString();
        TdeckSizeText.text = Tdeck.Count.ToString();
    }
}
