using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float jumpForce = 500f;
    [SerializeField]
    private float topBoundary = 10f;  //The highest the player can go before dying
    [SerializeField]
    private float bottomBoundary = -4f;
    [SerializeField]
    private string obstacleTag;
    
    private Rigidbody2D rb;

    public delegate void PlayerDiedEventHandler();
    public static event PlayerDiedEventHandler PlayerDied;

    //public event EventHandler PlayerDied;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
        }

        if (transform.position.y > topBoundary || transform.position.y < bottomBoundary )
        {
            Debug.Log("Player Died");
            OnPlayerDied();
            //SceneManager.LoadScene(0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == obstacleTag)
        {
            OnPlayerDied();
        }
    }

    protected virtual void OnPlayerDied()
    {
        PlayerDied?.Invoke();
    }
}
