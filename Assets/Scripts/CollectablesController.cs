using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseVariables;

public class CollectablesController : MonoBehaviour
{
    [SerializeField]
    private StringReference extraScoreTag;
    [SerializeField]
    private FloatReference score;
    [SerializeField]
    private GameObject[] collectibles;
    [SerializeField]
    private Vector3 spawnLocation;
    public Vector3 SpawnLocation { get => spawnLocation;  }

    [SerializeField]
    private float timeBetweenSpawns = 2f;
    private Timer spawnTimer;

    private bool playerDead = false;

    private void Awake()
    {
        PlayerController.PlayerDied += OnPlayerDied;
        DebugMessages.ClassInObjectSubscribed(this, "PlayerDied");
        spawnTimer = new Timer(timeBetweenSpawns);
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

    private void Update()
    {
        if (!playerDead)
        {
            //populate with collectable fish
            if (!spawnTimer.TimerCompleted)
            {
                spawnTimer.UpdateTimer(Time.deltaTime);
            }
            else
            {
                SpawnCollectible();
                spawnTimer.ResetTimer();
            }
        }
    }

    private IEnumerator AddExtraFishScore()
    {
        DebugMessages.CoroutineStarted(this);
        int index = 0;

        GameObject[] scoreFish = GameObject.FindGameObjectsWithTag(extraScoreTag);

        while (index < scoreFish.Length)
        {
            GameObject currentScoreFish = scoreFish[index];
            CluelessFishAI currentScoreFishAI = currentScoreFish.GetComponent<CluelessFishAI>();
            if (currentScoreFishAI.Collected)
            {
                currentScoreFishAI.AddCollectableToScore();
            }
            index++;
            yield return new WaitForSeconds(.5f);
        }

        DebugMessages.CoroutineEnded(this);
    }
    
    /*IEnumerator AddFishToScore(GameObject scoreFish, CluelessFishAI scoreFishAI, Vector3 targetPosition, float seconds)
    {
        DebugMessages.CoroutineStarted(this);
        Timer timer = new Timer(seconds);
        Vector3 startingPosition = scoreFish.transform.position;
        while (!timer.TimerCompleted)
        {
            scoreFish.transform.position = Vector3.Lerp(startingPosition, targetPosition, (timer.CurrentTime / timer.MaxTime));
            timer.UpdateTimer(Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        scoreFish.transform.position = targetPosition;
        score.Value += scoreFishAI.ScoreValue;
        DebugMessages.CoroutineEnded(this);
        //Destroy(scoreFish);
    }*/

    private void SpawnCollectible()
    {
        
        Instantiate(collectibles[Random.Range(0, collectibles.Length)], spawnLocation, Quaternion.identity);
    }

    private void OnPlayerDied()
    {
        playerDead = true;
        StartCoroutine(AddExtraFishScore());
    }
}
