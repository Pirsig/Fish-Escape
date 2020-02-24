using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseVariables;
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
    private StringReference obstacleTag;
    
    private Rigidbody2D rb;

    public delegate void PlayerDiedEventHandler();
    public static event PlayerDiedEventHandler PlayerDied;
    private bool playerDead;

    //public event EventHandler PlayerDied;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerDead = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !playerDead)
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
        DebugMessages.OriginatingEventFired(this, "OnPlayerDied()");
        PlayerDied?.Invoke();
        playerDead = true;
    }
}
