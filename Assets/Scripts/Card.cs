using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool hasbeenPlayed;

    public int handIndex;

    private GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void OnMouseDown()
    {
        if(hasbeenPlayed == false)
        {
            transform.position += Vector3.up * 5;
            hasbeenPlayed = true;
            gm.availableCardSlots[handIndex] = true;
            Invoke("MoveToDiscardPile", 2f);
        }
    }

    void MoveToDiscardPile()
    {
        gm.discardPile.Add(this);
        gameObject.SetActive(false);
    }
}