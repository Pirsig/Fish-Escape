using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAI : MonoBehaviour
{
    [SerializeField]
    private float baseSpeed = 1f;   //The base movement speed of the obstacle
    private float currentSpeed;  //the current speed for movement
    public float CurrentSpeed { get => currentSpeed; }
    [SerializeField]
    private float speedIncrease = 0.1f;    //The amount to increase the speed multiplier by when it is increased

    [SerializeField]
    private int maxObstaclesPassed = 15; //the amount of obstacles to pass before we increase the speed
    private int obstaclesPassed;

    [SerializeField]
    private float despawnPosition = -15f;  //the position at which the obstacle will despawn
    [SerializeField]
    private float respawnPosition = 15f;  //the position where the obstacle will respawn
    [SerializeField]
    private float randomYOffset = 0f;  //used as a range of negative to positive to offset the heighth of the obstacle
                                      //use 0 for no random offset
    [SerializeField]
    private float randomXOffset = 0f;    //used as a range of negative to positive to offset the left/right position of the obstacle
                                        //use 0 for no random offset

    private bool playerDead;

    public delegate void SpeedChangedEventHandler();
    public static event SpeedChangedEventHandler SpeedChanged;

    private void Awake()
    {
        currentSpeed = baseSpeed;
        obstaclesPassed = 0;
        playerDead = false;

        //Event Subscriptions

        PlayerController.PlayerDied += OnPlayerDied;
        DebugMessages.ClassInObjectSubscribed(this, "PlayerDied");

        ScoreZone.PlayerScored += OnPlayerScored;
        DebugMessages.ClassInObjectSubscribed(this, "PlayerScored");
    }

    private void OnDestroy()
    {
        //Unsubscribe to all events

        PlayerController.PlayerDied -= OnPlayerDied;
        DebugMessages.ClassInObjectUnsubscribed(this, "PlayerDied");

        ScoreZone.PlayerScored -= OnPlayerScored;
        DebugMessages.ClassInObjectUnsubscribed(this, "PlayerScored");
    }

    private void OnApplicationQuit()
    {
        //Unsubscribe to all events

        PlayerController.PlayerDied -= OnPlayerDied;
        DebugMessages.ClassInObjectUnsubscribed(this, "PlayerDied");

        ScoreZone.PlayerScored -= OnPlayerScored;
        DebugMessages.ClassInObjectUnsubscribed(this, "PlayerScored");
    }

    void Update()
    {
        if (!playerDead)
        {
            transform.position += Time.deltaTime * currentSpeed * Vector3.left;

            if (transform.position.x <= despawnPosition)
            {
                if (randomXOffset == 0 && randomYOffset == 0)
                {
                    RespawnObstacle();
                }
                else if (randomXOffset == 0 && randomYOffset != 0)
                {
                    RespawnObstacle(randomYOffset, 'y');
                }
                else if (randomXOffset != 0 && randomYOffset == 0)
                {
                    RespawnObstacle(randomXOffset, 'x');
                }
                else if (randomXOffset != 0 && randomYOffset != 0)
                {
                    RespawnObstacle(randomXOffset, randomYOffset);
                }
                else
                {
                    DebugMessages.ClassInObjectFatalError(this, "Method failed all checks for which version of respawnObstacle() to call");
                }
            }
        }
    }

    //moves the obstacle back to its respawn position along the x axis
    private void RespawnObstacle()
    {
        transform.position = new Vector3(respawnPosition, transform.position.y, transform.position.z);
    }

    /*DEPRECATED
    //moves the obstacle back to its respawn position along the x axis with a new random heighth in the range given
    private void RespawnObstacle(float randomYOffset)
    {
        float randomYPosition = Random.Range(-randomYOffset, randomYOffset);
        transform.position = new Vector3(respawnPosition, randomYPosition, transform.position.z);
    }*/

    //Moves the obstacle back to its respawn position along the x axis and applies a random offset on the specified axis
    private void RespawnObstacle(float randomOffset, char axis)
    {
        //ensures axis will always be a lower case char for consistent checking
        axis = char.ToLower(axis);

        float randomPosition = Random.Range(-randomOffset, randomOffset);

        switch (axis)
        {
            case 'x':
                transform.position = new Vector3(respawnPosition + randomPosition, transform.position.y, transform.position.z);
                break;
            case 'y':
                transform.position = new Vector3(respawnPosition, randomPosition, transform.position.z);
                break;
            case 'z':
                //don't actually use this since this shouldn't do anything but mess up viewing order in 2d
                transform.position = new Vector3(respawnPosition, transform.position.y, randomPosition);
                break;
            default:
                DebugMessages.ClassInObjectFatalError(this, "RespawnObstacle(char axis) must be x, y, or z");
                break;
        }
    }

    //Moves the obstacle back to a random offset from the respawn position along the x axis, and a random heighth along y
    private void RespawnObstacle(float randomXOffset, float randomYOffset)
    {
        float randomXPosition = Random.Range(-randomXOffset, randomXOffset);
        float randomYPosition = Random.Range(-randomYOffset, randomXOffset);
        transform.position = new Vector3(respawnPosition + randomXPosition, randomYPosition, transform.position.z);
    }

    //EVENT FUNCTIONS

    private void OnPlayerDied()
    {
        DebugMessages.EventFired(this, "OnPlayerDied()");
        playerDead = true;
    }

    private void OnPlayerScored()
    {
        DebugMessages.EventFired(this, "OnPlayerScored()");
        obstaclesPassed++;
        if (obstaclesPassed >= maxObstaclesPassed)
        {
            currentSpeed += speedIncrease;
            obstaclesPassed = 0;
        }
    }

    protected virtual void OnSpeedChanged()
    {
        DebugMessages.OriginatingEventFired(this, "OnSpeedChanged()");
        SpeedChanged?.Invoke();
    }
}
