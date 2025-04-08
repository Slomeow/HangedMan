using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    public GameObject overlaySprite; // Drag the overlay sprite GameObject here in the Inspector

    private void OnMouseEnter()
    {
        if (overlaySprite != null)
        {
            overlaySprite.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        if (overlaySprite != null)
        {
            overlaySprite.SetActive(false);
        }
    }
}