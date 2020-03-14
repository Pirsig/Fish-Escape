using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseVariables;

public class PlayerController : MonoBehaviour
{
    [Header ("Movement")]
    [SerializeField]
    private float jumpForce = 500f;
    [SerializeField]
    private float topBoundary = 10f;  //The highest the player can go before dying

    [Header("Sound")]
    [SerializeField]
    private AudioClip[] jumpSounds;
    private int currentJumpSound = 0;  //The index for the current sound in the jumpSounds array that we want to play, always starts at 0
    [SerializeField]
    private AudioClip deathSound;
    private SoundController soundController;
    private BGMController bgmController;

    [Header ("Obstacles")]
    [SerializeField]
    private StringReference obstacleTag;
    private Rigidbody2D rb;

    //Events
    public delegate void PlayerDiedEventHandler();
    public static event PlayerDiedEventHandler PlayerDied;
    private bool playerDead;
    public bool PlayerDead { get => playerDead; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerDead = false;
    }

    private void Start()
    {
        bgmController = BGMController.FindBGMController();
        soundController = SoundController.FindSoundController();
    }

    private void Update()
    {
        if (!playerDead)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                PlayJumpSound();
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * jumpForce);
            }

            if (transform.position.y > topBoundary)
            {
                Debug.Log("Player Died");
                OnPlayerDied();
            }
        }
    }

    private void PlayJumpSound()
    {
        if (soundController != null && jumpSounds != null)
        {
            soundController.PlaySound(jumpSounds[currentJumpSound]);
            currentJumpSound++;
            if (currentJumpSound >= jumpSounds.Length)
            {
                currentJumpSound = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!playerDead)
        {
            if (collision.gameObject.tag == obstacleTag)
            {
                OnPlayerDied();
            }
        }
    }

    protected virtual void OnPlayerDied()
    {
        DebugMessages.OriginatingEventFired(this, "OnPlayerDied()");

        playerDead = true;
        if(soundController != null && deathSound != null)
        {
            soundController.PlaySound(deathSound);
        }
        if(bgmController != null)
        {
            bgmController.ChangeMusicTrack(2);
        }

        PlayerDied?.Invoke();
    }
}
