using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel: MonoBehaviour
{
    [SerializeField] private AudioSource BGMusic;
    [SerializeField] private Rigidbody2D playerRB;

    private AudioSource finishSound;
    private Animator anim;

    private bool levelCompleted = false;

    private void Start()
    {
        finishSound = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !levelCompleted)
        {
            anim.SetTrigger("endLevel");
            BGMusic.Stop();
            finishSound.Play();
            levelCompleted = true;
            Invoke("CompleteLevel", 5f);
        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene("EndScreen");
    }
}