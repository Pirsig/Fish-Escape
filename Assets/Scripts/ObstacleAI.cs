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
    private float randomOffset = 2f;  //used as a range of negative to positive to offset the heighth of the obstacle

    private bool playerDead;

    void Update()
    {
        if (!playerDead)
        {
            transform.position += Time.deltaTime * speed * Vector3.left;
        }
        

        if (transform.position.x <= despawnPosition)
        {
            if (randomOffset == 0)
            {
                RespawnObstacle();
            }
            else
            {
                RespawnObstacle(randomOffset);
            }
        }
    }

    //moves the obstacle back to it's starting position along the x axis
    private void RespawnObstacle()
    {
        transform.position = new Vector3(respawnPosition, transform.position.y, transform.position.z);
    }

    //moves the obstacle back to it's starting position along the x axis with a new random heighth in the range given
    private void RespawnObstacle(float respawnRandomOffset)
    {
        float randomHeight = Random.Range(-respawnRandomOffset, respawnRandomOffset);
        transform.position = new Vector3(respawnPosition, randomHeight, transform.position.z);
    }
}
