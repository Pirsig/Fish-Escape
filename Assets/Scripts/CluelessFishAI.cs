﻿using System.Collections;
using UnityEngine;
using BaseVariables;

public class CluelessFishAI : MonoBehaviour
{
    [Header("Movement & Spawning")]
    //The movement speed of the fish, should be slower than the player
    [SerializeField]
    private float speed;
    //The range the fish can move up and down while swimming around
    [SerializeField]
    private float ySwimmingRange;
    //the lower and upper boundaries of where fish are allowed to move
    [SerializeField]
    private Vector2 yBoundaries;
    private float currentYPosition;
    //which boundary was violated; false for lower, true for upper
    private bool lowerOrUpperBoundary;
    //The range at which the fish will hang behind the player once collected
    [SerializeField]
    private float xBehindPlayerRange;
    [SerializeField]
    private float xOffsetBehindPlayer = 2.5f;
    [SerializeField]
    private float movementTransitionTime = 2f;

    //The time to wait before setting a new y position
    [SerializeField]
    private float yChangeTime = 2f;
    private Timer yChangeTimer;
    private float randomYPosition;


    private SoundController soundController;
    [Header("Sound")]
    [SerializeField]
    private AudioClip collectedSound;
    [SerializeField]
    private AudioClip scoreoffSound;

    [Header("Player Info")]
    //the tag used to identify the player
    [SerializeField]
    private StringReference playerTag;
    private GameObject player;
    //Has the player collected the fish
    private bool collected = false;
    public bool Collected { get => collected; }
    //The value of the fish when added to the player's score
    [SerializeField]
    private int scoreValue;
    public int ScoreValue { get => scoreValue; }
    [SerializeField]
    private FloatReference playerScore;
    
    private CollectablesController controller;

    //bool for controlling whether or not the boundaries for follow the player are top right and bottom left or top left and bottom right
    private bool flipMovementBoundaries = false;

    private void Awake()
    {
        yChangeTimer = new Timer(yChangeTime);
        randomYPosition = UnityEngine.Random.Range(-ySwimmingRange, ySwimmingRange);
        player = GameObject.FindGameObjectWithTag(playerTag);
        controller = GameObject.Find("CollectablesController").GetComponent<CollectablesController>();
    }

    private void Start()
    {
        soundController = SoundController.FindSoundController();
    }

    private void Update()
    {
        currentYPosition = transform.position.y;
        //move randomly up and down drifting back towards and eventually behind the player
        if (!collected)
        {
            yChangeTimer.UpdateTimer(Time.deltaTime);
            if (IsFishAtYBoundary(out lowerOrUpperBoundary))
            {
                if(!lowerOrUpperBoundary)
                {
                    //lower boundary violated
                    randomYPosition = -ySwimmingRange;
                }
                else
                {
                    //upper boundary violated
                    randomYPosition = ySwimmingRange;
                }
                yChangeTimer.ResetTimer();
            }
            if (yChangeTimer.TimerCompleted)
            {
                randomYPosition = UnityEngine.Random.Range(-ySwimmingRange, ySwimmingRange);
                yChangeTimer.ResetTimer();
            }
            //move
            transform.position += Time.deltaTime * speed * new Vector3(1, randomYPosition, 0);
            
            //destroy the object if it is past the point where obstacles despawn
            if (transform.position.x < -controller.SpawnLocation.x)
            {
                DebugMessages.MethodInClassDestroyObject(this, this.gameObject);
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == playerTag && !collected && !player.GetComponent<PlayerController>().PlayerDead)
        {
            Debug.LogWarning(gameObject.name + " has collided with player!");
            if (soundController != null)
            {
                soundController.PlaySound(collectedSound);
            }
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

    private IEnumerator StartFollowingPlayer()
    {
        DebugMessages.CoroutineStarted(this);
        //int debugCounter = 0;
        Vector3 transitionStartPosition = transform.position;
        Vector3 transitionTargetPosition = new Vector3((-xOffsetBehindPlayer - UnityEngine.Random.Range(0f, xBehindPlayerRange)), randomYPosition, 0);
        DebugMessages.SimpleVariableOutput(this, transitionStartPosition, nameof(transitionStartPosition));
        DebugMessages.SimpleVariableOutput(this, transitionTargetPosition, nameof(transitionTargetPosition));
        Timer moveTimer = new Timer(movementTransitionTime);
        while (!moveTimer.TimerCompleted)
        {
            transform.position = Vector3.Lerp(transitionStartPosition, transitionTargetPosition, moveTimer.CurrentTime / moveTimer.MaxTime);
            moveTimer.UpdateTimer(Time.deltaTime);
            //debugCounter++;
            //DebugMessages.SimpleMethodOutput(this, debugCounter, "debugCounter");
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine("FollowPlayer");
        DebugMessages.CoroutineEnded(this);
        //Debug.LogWarning("StartFollowingPlayer() coroutine has ended!");
    }

    //mimics loosely following the player by creating boundaries based on the player's current position as well as
    //xOffsetBehindPlayer, xBehindPlayerRange, and ySwimmingRange
    private IEnumerator FollowPlayer()
    {
        DebugMessages.CoroutineStarted(this);

        //Timer for how long we want to take for each movement
        Timer changeMovementTimer = new Timer(movementTransitionTime);

        Vector3 startPosition = transform.position;
        //The lower boundary for choosing a new follow position
        Vector3 lowerFollowBoundary;
        //The upper boudnary for choosing a new follow position
        Vector3 upperFollowBoundary;

        //random percent to lerp for choosing a new position
        float randomPos = Random.Range(0f, 1f);
        //Define our new boundaries based on the player's current location
        if (!flipMovementBoundaries)
        {
            lowerFollowBoundary = new Vector3(-xOffsetBehindPlayer - xBehindPlayerRange, player.transform.position.y - ySwimmingRange, 0);
            upperFollowBoundary = new Vector3(-xOffsetBehindPlayer, player.transform.position.y + ySwimmingRange, 0);
            flipMovementBoundaries = true;
        }
        else
        {
            lowerFollowBoundary = new Vector3(-xOffsetBehindPlayer, player.transform.position.y - ySwimmingRange, 0);
            upperFollowBoundary = new Vector3(-xOffsetBehindPlayer - xBehindPlayerRange, player.transform.position.y + ySwimmingRange, 0);
            flipMovementBoundaries = false;
        }

        //Choose a new position to go to within our boundaries
        Vector3 newPosition = Vector3.Lerp(lowerFollowBoundary, upperFollowBoundary, randomPos);

        //Moves from startPosition to newPosition over the length of changeMovementTimer
        while (!changeMovementTimer.TimerCompleted)
        {
            transform.position = Vector3.Lerp(startPosition, newPosition, changeMovementTimer.CurrentTime / changeMovementTimer.MaxTime);
            changeMovementTimer.UpdateTimer(Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        //restarts the coroutine so we continue moving
        StartCoroutine("FollowPlayer");
        DebugMessages.CoroutineEnded(this);
    }
    

    //Public method to start the  AddToScore coroutine with some default values
    public void AddCollectableToScore()
    {
        StartCoroutine(AddToScore(new Vector3(10f, 10f, 0f), 1.5f));
    }

    //Public method to start the AddToScore coroutine with custom values
    public void AddCollectableToScore(Vector3 targetPosition, float timeToMove)
    {
        StartCoroutine(AddToScore(targetPosition, timeToMove));
    }

    //moves the collectible to targetPosition over timeToMove and then adds its coreValue to playerScore
    private IEnumerator AddToScore(Vector3 targetPosition, float timeToMove)
    {
        DebugMessages.CoroutineStarted(this);
        Timer timer = new Timer(timeToMove);
        Vector3 startingPosition = transform.position;
        //moves the object over timeToMove to the targetPosition
        while (!timer.TimerCompleted)
        {
            transform.position = Vector3.Lerp(startingPosition, targetPosition, (timer.CurrentTime / timer.MaxTime));
            timer.UpdateTimer(Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        //moves us definitively to targetPosition, just in case
        transform.position = targetPosition;
        //adds the objects scoreValue to the player's score
        playerScore.Value += scoreValue;
        soundController.PlaySound(scoreoffSound);
        DebugMessages.CoroutineEnded(this);
        DebugMessages.MethodInClassDestroyObject(this, this.gameObject);
        Destroy(this.gameObject);
    }

    //checks if the fish is at the lower or upper y boundary
    //also outputs a bool for where it is the lower or upper boundary being violated
    //false for lower, true for upper
    private bool IsFishAtYBoundary(out bool upOrDown)
    {
        if (currentYPosition <= yBoundaries.x)
        {
            upOrDown = false;
            return true;
        }
        else if (currentYPosition >= yBoundaries.y)
        {
            upOrDown = true;
            return true;
        }
        else
        {
            upOrDown = false;
            return false;
        }
    }
}
