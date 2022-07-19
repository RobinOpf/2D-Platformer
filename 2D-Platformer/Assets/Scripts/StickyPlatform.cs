using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hello");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Helloooooooo");
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Bye");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Byeeeeeee");
            collision.gameObject.transform.SetParent(null);
        }
    }
}