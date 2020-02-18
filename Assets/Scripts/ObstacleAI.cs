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

    void Update()
    {
        transform.position += Time.deltaTime * speed * Vector3.left;
        if (transform.position.x <= despawnPosition)
        {
            if (randomOffset == 0)
            {
                transform.position = new Vector3(respawnPosition, transform.position.y, transform.position.z);
            }
            else
            {
                float randomHeight = UnityEngine.Random.Range(-randomOffset, randomOffset);
                transform.position = new Vector3(respawnPosition, randomHeight, transform.position.z);
            }
        }
    }
}
