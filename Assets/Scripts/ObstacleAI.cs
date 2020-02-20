using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAI : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;  //Speed multiplier for movement
    [SerializeField]
    private float despawnPosition = -15f;  //the position at which the obstacle will despawn
    [SerializeField]
    private float respawnPosition = 15f;  //the position where the obstacle will respawn
    [SerializeField]
    private float randomYOffset = 2f;  //used as a range of negative to positive to offset the heighth of the obstacle
                                      //use 0 for no random offset
    [SerializeField]
    private float randomXOffset = 0;    //used as a range of negative to positive to offset the left/right position of the obstacle
                                        //use 0 for no random offset

    private bool playerDead;

    private void Awake()
    {
        playerDead = false;
        PlayerController.PlayerDied += OnPlayerDied;
        DebugMessages.ClassInObjectSubscribed(this, "PlayerDied");
    }

    private void OnDestroy()
    {
        PlayerController.PlayerDied -= OnPlayerDied;
        DebugMessages.ClassInObjectUnsubscribed(this, "PlayerDied");
    }

    private void OnApplicationQuit()
    {
        PlayerController.PlayerDied -= OnPlayerDied;
        DebugMessages.ClassInObjectUnsubscribed(this, "PlayerDied");
    }

    void Update()
    {
        if (!playerDead)
        {
            transform.position += Time.deltaTime * speed * Vector3.left;

            if (transform.position.x <= despawnPosition)
            {
                if (randomYOffset == 0 && randomXOffset == 0)
                {
                    RespawnObstacle();
                }
                else if (randomXOffset == 0 && randomYOffset != 0)
                {
                    RespawnObstacle(randomYOffset);
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

    //moves the obstacle back to its respawn position along the x axis with a new random heighth in the range given
    private void RespawnObstacle(float randomYOffset)
    {
        float randomYPosition = Random.Range(-randomYOffset, randomYOffset);
        transform.position = new Vector3(respawnPosition, randomYPosition, transform.position.z);
    }

    //Moves the obstacle back to a random offset from the respawn position along the x axis, and a random heighth along y
    private void RespawnObstacle(float randomXOffset, float randomYOffset)
    {
        float randomXPosition = Random.Range(-randomXOffset, randomXOffset);
        float randomYPosition = Random.Range(-randomYOffset, randomXOffset);
        transform.position = new Vector3(respawnPosition + randomXPosition, randomYPosition, transform.position.z);
    }

    private void OnPlayerDied()
    {
        DebugMessages.EventFired(this, "OnPlayerDied()");
        playerDead = true;
    }
}
