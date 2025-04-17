using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tarot : MonoBehaviour
{
    public bool ThasbeenPlayed;

    public int ThandIndex;

    private GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void OnMouseDown()
    {
        if(ThasbeenPlayed == false)
        {
            transform.position += Vector3.up * 5;
            ThasbeenPlayed = true;
            gm.availableTarotSlots[ThandIndex] = true;
            Invoke("TMoveToDiscardPile", 2f);
        }
    }

    void TMoveToDiscardPile()
    {
        gm.TdiscardPile.Add(this);
        gameObject.SetActive(false);
    }
}