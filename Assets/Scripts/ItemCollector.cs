using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int cherries = 0;

    [SerializeField] private Text cherriesText;

    [SerializeField] private AudioSource collectionSoundEffect;

    //jeœli dotknie do obiektu
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Money"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            cherries++;
            cherriesText.text = "Money: " + cherries;
        }
    }
}
