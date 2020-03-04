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
    //[SerializeField]
    //private float bottomBoundary = -4f;
    [SerializeField]
    private StringReference obstacleTag;
    //[SerializeField]
    //private StringReference extraScoreTag;
    //[SerializeField]
    //private FloatReference score;
    
    private Rigidbody2D rb;

    public delegate void PlayerDiedEventHandler();
    public static event PlayerDiedEventHandler PlayerDied;
    private bool playerDead;
    public bool PlayerDead { get => playerDead; }

    //public event EventHandler PlayerDied;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerDead = false;
    }

    private void Update()
    {
        if (!playerDead)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * jumpForce);
            }

            if (transform.position.y > topBoundary)
            {
                Debug.Log("Player Died");
                OnPlayerDied();
            }
        }
        /*if (transform.position.y > topBoundary || transform.position.y < bottomBoundary )
        {
            Debug.Log("Player Died");
            OnPlayerDied();
            //SceneManager.LoadScene(0);
        }*/
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
        
        PlayerDied?.Invoke();
    }

    /*IEnumerator AddExtraFishScore()
    {
        DebugMessages.CoroutineStarted(this);
        int index = 0;

        GameObject[] scoreFish = GameObject.FindGameObjectsWithTag(extraScoreTag);

        while (index < scoreFish.Length)
        {
            GameObject currentScoreFish = scoreFish[index];
            CluelessFishAI currentScoreFishAI = currentScoreFish.GetComponent<CluelessFishAI>();
            if(currentScoreFishAI.Collected)
            {
                StartCoroutine(AddFishToScore(currentScoreFish, currentScoreFishAI, new Vector3(10f, 10f, 0f), 1.5f));
            }
            index++;
            yield return new WaitForSeconds(.5f);
        }

        DebugMessages.CoroutineEnded(this);
    }
    

    IEnumerator AddFishToScore(GameObject scoreFish, CluelessFishAI scoreFishAI, Vector3 targetPosition, float seconds)
    {
        DebugMessages.CoroutineStarted(this);
        Timer timer = new Timer(seconds);
        Vector3 startingPosition = scoreFish.transform.position;
        while (timer.CurrentTime < timer.MaxTime)
        {
            scoreFish.transform.position = Vector3.Lerp(startingPosition, targetPosition, (timer.CurrentTime / timer.MaxTime));
            timer.UpdateTimer(Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        scoreFish.transform.position = targetPosition;
        score.Value += scoreFishAI.ScoreValue;
        Destroy(scoreFish);
        DebugMessages.CoroutineEnded(this);
    }*/
}
