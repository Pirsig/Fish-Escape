﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CluelessFishAI : MonoBehaviour
{
    //The value of the fish when added to the player's score
    [SerializeField]
    private int scoreValue;
    //The movement speed of the fish, should be slower than the player
    [SerializeField]
    private float speed;
    //The range the fish can move up and down while swimming around
    [SerializeField]
    private float ySwimmingRange;
    //The range at which the fish will hang behind the player once collected
    [SerializeField]
    private float xBehindPlayerRange;
    [SerializeField]
    private float collectionTransitionTime = 2f;
    private bool transitionCompleted;

    //Has the player collected the fish
    private bool collected = false;

    //The time to wait before setting a new y position
    [SerializeField]
    private float yChangeTime = 2f;
    private Timer yChangeTimer;
    private float randomYPosition;
    
    //the tag used to identify the player
    [SerializeField]
    private string playerTag;

    private void Awake()
    {
        yChangeTimer = new Timer(yChangeTime);
        randomYPosition = UnityEngine.Random.Range(-ySwimmingRange, ySwimmingRange);
    }

    private void Update()
    {
        
        if(!collected)
        {
            yChangeTimer.UpdateTimer(Time.deltaTime);
            if(yChangeTimer.TimerCompleted)
            {
                randomYPosition = UnityEngine.Random.Range(-ySwimmingRange, ySwimmingRange);
                yChangeTimer.ResetTimer();
            }
            transform.position += Time.deltaTime * speed * new Vector3(1, randomYPosition, 0); 
        }
        else if (collected && transitionCompleted)
        {
            //move in a way that follows the player
            Debug.Log("movement following goes here");
        }
        else
        {
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == playerTag && !collected)
        {
            Debug.LogWarning(gameObject.name + " has collided with player!");
            collected = true;
            StartCoroutine("StartFollowingPlayer");
        }
    }

    /*public IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 target, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, target, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToMove.transform.position = target;
    }*/

    public IEnumerator StartFollowingPlayer()
    {
        //int debugCounter = 0;
        Vector3 transitionStartPosition = transform.position;
        Vector3 transitionTargetPosition = new Vector3( (-2.5f - UnityEngine.Random.Range(0f, xBehindPlayerRange)), randomYPosition, 0);
        Timer moveTimer = new Timer(collectionTransitionTime);
        while(!moveTimer.TimerCompleted)
        {
            transform.position = Vector3.Lerp(transitionStartPosition, transitionTargetPosition, moveTimer.CurrentTime / moveTimer.MaxTime);
            moveTimer.UpdateTimer(Time.deltaTime);
            //debugCounter++;
            //DebugMessages.SimpleMethodOutput(this, debugCounter, "debugCounter");
            yield return new WaitForEndOfFrame();
        }
        transitionCompleted = true;
        Debug.LogWarning("StartFollowingPlayer() coroutine has ended!");
    }
}