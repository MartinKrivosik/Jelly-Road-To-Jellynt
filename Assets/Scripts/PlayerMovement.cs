using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    public static int currentLevel = 1;

    private enum MovementState { idle, running, jumping, falling}

    [SerializeField] private AudioSource jumpSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
       
        
         
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSound.Play();
            GetComponent<Rigidbody2D>().velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState();

    }


    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }


        anim.SetInteger("State", (int)state);

    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeathBox"))
        {
            if (CoinCollector.score > CoinCollector.highScore)
            {
                CoinCollector.highScore = CoinCollector.score;
                Debug.Log(CoinCollector.highScore);
            }
            collision.gameObject.SetActive(false);
            PlayerLife.currentHealth = Mathf.Clamp(PlayerLife.currentHealth - 1, 0f, PlayerLife.startingHealth);
            Invoke("RestartLevel", 1f);
            
        }

    }

    private void RestartLevel()
    {
        CoinCollector.score = CoinCollector.globalScore;
        if (PlayerMovement.currentLevel == 1)
        {
            if (PlayerLife.currentHealth == 2)
            {
                SceneManager.LoadScene("Level 1 - 2 (Yellow)");
            }
            if (PlayerLife.currentHealth == 1)
            {
                SceneManager.LoadScene("Level 1 - 3 (Red)");
            }
            if (PlayerLife.currentHealth == 0)
            {
                SceneManager.LoadScene("EndScreen");
            }
        }
        if (PlayerMovement.currentLevel == 2)
        {
            if (PlayerLife.currentHealth == 2)
            {
                SceneManager.LoadScene("Level 2 - 2 (Yellow)");
            }
            if (PlayerLife.currentHealth == 1)
            {
                SceneManager.LoadScene("Level 2 - 3 (Red)");
            }
            if (PlayerLife.currentHealth == 0)
            {
                SceneManager.LoadScene("EndScreen");
            }

        }

    }

}
