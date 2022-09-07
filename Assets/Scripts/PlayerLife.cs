using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    public static float startingHealth = 3;
    public static float currentHealth = 3;
    public static bool isDead;

    [SerializeField] private AudioSource deathSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        isDead = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(sprite.CompareTag("Green") && (collision.gameObject.CompareTag("Yellow") || collision.gameObject.CompareTag("Red")) && !isDead)
        {
            isDead = true;
            Die();
        }

        if (sprite.CompareTag("Yellow") && (collision.gameObject.CompareTag("Red") || collision.gameObject.CompareTag("Skull")) && !isDead)
        {
            isDead = true;
            Die();
        }
        if (sprite.CompareTag("Red") && collision.gameObject.CompareTag("Skull") && !isDead)
        {
            isDead = true;
            Die();
        }

    }


    public void Die()
    {
        deathSound.Play();
        if (CoinCollector.score > CoinCollector.highScore)
        {
            CoinCollector.highScore = CoinCollector.score;
        }
        currentHealth = Mathf.Clamp(currentHealth - 1, 0f, startingHealth);
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("Death");
    }

    private void RestartLevel()
    {
        CoinCollector.score = CoinCollector.globalScore;
        isDead = false;

        if (PlayerMovement.currentLevel == 1)
        {
            if (currentHealth == 2)
            {
                SceneManager.LoadScene("Level 1 - 2 (Yellow)");
            }
            if (currentHealth == 1)
            {
                SceneManager.LoadScene("Level 1 - 3 (Red)");
            }
            if (currentHealth == 0)
            {
                SceneManager.LoadScene("EndScreen");
            }
        }
        if (PlayerMovement.currentLevel == 2)
        {
            if (currentHealth == 2)
            {
                SceneManager.LoadScene("Level 2 - 2 (Yellow)");
            }
            if (currentHealth == 1)
            {
                SceneManager.LoadScene("Level 2 - 3 (Red)");
            }
            if (currentHealth == 0)
            {
                SceneManager.LoadScene("EndScreen");
            }

        }
    }
}
