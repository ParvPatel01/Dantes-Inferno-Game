using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public AudioClip collectSound;

    public GemBar gemBar;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.instance.PlaySound(collectSound);
            gemBar.AddGem();
            Destroy(gameObject);
        }
    }
}
