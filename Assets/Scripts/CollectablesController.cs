using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseVariables;
using System.Linq;

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

    [Header("End of round scoring")]
    [SerializeField]
    private Vector3 scoreOffPosition;
    [SerializeField]
    private float timeToScore;

    [Header("High Score entry screen")][SerializeField]
    private GameObject HighScoreEntryDisplay;

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
        //populates a new list with all the collectibles currently active
        List<GameObject> collectedObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag(extraScoreTag).ToList());
        while(index < collectedObjects.Count)
        {
            Debug.LogWarning(collectedObjects[index].ToString());
            index++;
        }
        Debug.LogWarning(collectedObjects.ToString());
        //removes all objects from the list that were not collected
        collectedObjects.RemoveAll(GameObject => !GameObject.GetComponent<CluelessFishAI>().Collected);
        index = 0;
        while (index < collectedObjects.Count)
        {
            GameObject currentScoreFish = collectedObjects[index];
            //if the object is null we move onto the next object in the array
            //this prevents problems cause by fish that despawned after the array's formation that were not collected, and thus still moving backwards
            if (currentScoreFish == null)
            {
                index++;
                continue;
            }
            CluelessFishAI currentScoreFishAI = currentScoreFish.GetComponent<CluelessFishAI>();
            if (currentScoreFishAI.Collected)
            {
                currentScoreFishAI.AddCollectableToScore(scoreOffPosition, timeToScore);
            }
            index++;
            yield return new WaitForSeconds(.5f);
        }
        CheckForNewHighScore(score);
        DebugMessages.CoroutineEnded(this);
    }

    private void CheckForNewHighScore(float score)
    {
        HighScore[] highScores;
        try
        {
            highScores = SaveManager.LoadHighScores();
        }
        catch (System.IO.FileNotFoundException exception)
        {
            Debug.LogWarning("No save file was found for high scores");
            Debug.LogWarning(exception);
            //fills highScores with an empty array so it will make a new high score list
            highScores = new HighScore[10];

        }

        if (HighScore.IsHighScoreNew(score, highScores))
        {
            Debug.Log("New High Score!");
            //convert score into a new HighScore with the players name and write it to the disk
            //HighScore newHighScore = new HighScore(score);

            //take in player's name
            GameObject temp = Instantiate(HighScoreEntryDisplay, GameObject.Find("GameplayUI").transform);
            temp.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 15);
            //write new highscore to the disk
            //highScores = HighScore.SortHighScores(newHighScore, highScores);
            //SaveManager.SaveHighScores(highScores);
        }
        else
        {
            Debug.Log("No new high score :(");
        }
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
        
        Instantiate(collectibles[UnityEngine.Random.Range(0, collectibles.Length)], spawnLocation, Quaternion.identity);
    }

    private void OnPlayerDied()
    {
        playerDead = true;
        StartCoroutine(AddExtraFishScore());
    }
}
