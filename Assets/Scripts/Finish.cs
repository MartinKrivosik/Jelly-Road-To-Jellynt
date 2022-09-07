using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{

    private bool levelCompleted = false;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !levelCompleted)
        {
            collision.gameObject.SetActive(false);
            CoinCollector.globalScore = CoinCollector.score;
            CoinCollector.highScore = CoinCollector.globalScore;
            levelCompleted = true;
            Invoke("CompleteLevel", 2f);
        }
    }

    private void CompleteLevel()
    {
        if (PlayerMovement.currentLevel == 1)
        {
            PlayerMovement.currentLevel++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
        } 
        else
        {
            SceneManager.LoadScene("EndScreen");
        }
       
    }

}
