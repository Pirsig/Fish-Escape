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
    private GameObject[] Collectables;

    private void Awake()
    {
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

    IEnumerator AddExtraFishScore()
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
    }

    private void OnPlayerDied()
    {
        StartCoroutine(AddExtraFishScore());
    }
}
