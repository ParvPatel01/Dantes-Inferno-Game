using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemBar : MonoBehaviour
{
    private int gemCount;
    private int maxGemCount = 3;

    [SerializeField] private Image gem1;
    [SerializeField] private Image gem2;
    [SerializeField] private Image gem3;

    public DeathUIScript deathScreen;


    private void Start()
    {
        gemCount = 0;
        gem1.enabled = false;
        gem2.enabled = false;
        gem3.enabled = false;
    }

    public void AddGem()
    {
        gemCount++;
        if (gemCount >= maxGemCount)
        {
            deathScreen.ShowDeathScreen();
            deathScreen.SetText("You Win!");
            gemCount = maxGemCount;
        }
        if (gemCount == 1)
        {
            gem1.enabled = true;
        }
        else if (gemCount == 2)
        {
            gem2.enabled = true;
        }
        else if (gemCount == 3)
        {
            gem3.enabled = true;
        }
    }
}
