using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CluelessFishAI : MonoBehaviour
{
    [SerializeField]
    private float speed;
    //The range the fish can move up and down while swimming around
    [SerializeField]
    private float ySwimmingRange;
    //The range at which the fish will hang behind the player once collected
    [SerializeField]
    private float xBehindPlayerRange;

    //Has the player collected the fish
    private bool collected = false;
    
    //the tag used to identify the player
    private string playerTag;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == playerTag)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        throw new NotImplementedException();
    }
}
