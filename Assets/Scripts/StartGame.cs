using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartGame : MonoBehaviour
{
  public void StartFirstLevel()
    {
        CoinCollector.globalScore = 0;
        CoinCollector.score = 0;
        PlayerLife.currentHealth = 3;
        PlayerMovement.currentLevel = 1;
        SceneManager.LoadScene("Level 1 - 1 (Green)");
    }
    public void Controls()
    {
        SceneManager.LoadScene("Controls");
    }
}
