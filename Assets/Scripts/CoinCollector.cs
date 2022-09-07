using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CoinCollector : MonoBehaviour
{

    public static int score = 0;
    public static int globalScore = 0;
    public static int highScore = 0;
    [SerializeField] private Text scoreCounter;

    [SerializeField] private AudioSource collectSound;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("EndScreen"))
        {
            scoreCounter.text = "High score: " + highScore;
        }
        else
        {
            scoreCounter.text = "Score: " + globalScore;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            collectSound.Play();
            Destroy(collision.gameObject);
            score++;
            scoreCounter.text = "Score:" + score;
        }
    }
}
