using System;
using UnityEngine;

public class PlayerCollison : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 5f;
    int id;

    private GameManager gameManager;
    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            gameManager.AddScore(1);
            AudioManager.Instance.PlayCollectSound();
        }
        else if (collision.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
            AudioManager.Instance.PlayCollectSound();
            gameManager.AddScore(10);
            gameManager.GameWin();
        }
        else if (collision.CompareTag("Trap"))
        {
            gameManager.GameOver();
        }
        //else if (collision.CompareTag("Enemy"))
        //{
        //    gameManager.GameOver();
        //}
        else if (collision.CompareTag("Enemy"))
        {
            // Check if player is above the enemy (like stomping in Mario)
            if (transform.position.y > collision.transform.position.y + 0.5f) // Adjust 0.5f based on enemy height
            {
                // Bounce the player upward
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                AudioManager.Instance.PlayEnemyDefeatSound();
                // Destroy the enemy
                Destroy(collision.gameObject);
                gameManager.AddScore(5);
                Debug.Log("Enemy defeated!");
            }
            else
                {
                    gameManager.GameOver();
                }
        }
        else if (collision.CompareTag("Deadzone"))
        {
            gameManager.GameOver();
        }
    }
}
