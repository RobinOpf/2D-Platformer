using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    

    [SerializeField] private Text collectiblesText;

    [SerializeField] private AudioSource collectionSoundEffect;

    private void Start()
    {
        collectiblesText.text = ": " + ApplicationModel.collectibles;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            ApplicationModel.collectibles++;
            collectiblesText.text = ": " + ApplicationModel.collectibles;
        }
    }
}
