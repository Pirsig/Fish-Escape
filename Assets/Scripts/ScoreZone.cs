using UnityEngine;
using BaseVariables;

public class ScoreZone : MonoBehaviour
{
    [SerializeField]
    private string playerTag = "Player";

    //Stores the base value for the player passing through the score zone.
    [SerializeField]
    private float zoneScoreValue = 10f;

    //the player's current score
    public FloatReference score;

    //Multiplier to increase score value as we go further into the game
    public float scoreMultiplier = 1f;
    public float scoreMultiplierIncrease = 0.5f;

    public delegate void PlayerScoredEventHandler();
    public static event PlayerScoredEventHandler PlayerScored;
    
    private float timeFromLastSpeedChanged = 0;
    private bool recentOnSpeedChanged = false;

    private void Awake()
    {
        score.Value = 0;

        //Subscribe to events

        ObstacleAI.SpeedChanged += OnSpeedChanged;
        DebugMessages.ClassInObjectSubscribed(this, "SpeedChanged");
    }

    private void Update()
    {
        if(recentOnSpeedChanged)
        {
            if(timeFromLastSpeedChanged<1)
            {
                timeFromLastSpeedChanged += Time.deltaTime;
            }
            else
            {
                recentOnSpeedChanged = false;
            }
        }
    }

    private void OnDestroy()
    {
        //Unsubscribe to all events

        ObstacleAI.SpeedChanged -= OnSpeedChanged;
        DebugMessages.ClassInObjectUnsubscribed(this, "SpeedChanged");
    }

    private void OnApplicationQuit()
    {
        //Unsubscribe to all events

        ObstacleAI.SpeedChanged -= OnSpeedChanged;
        DebugMessages.ClassInObjectUnsubscribed(this, "SpeedChanged");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == playerTag)
        {
            score.Value += ScoreValue(zoneScoreValue);
            Debug.Log("score = " + score);
            OnPlayerScored();
        }
    }

    protected virtual void OnPlayerScored()
    {
        DebugMessages.OriginatingEventFired(this, "PlayerScored");
        PlayerScored?.Invoke();
    }

    private void OnSpeedChanged()
    {
        if(!recentOnSpeedChanged)
        {
            DebugMessages.EventFired(this, "SpeedChanged");
            scoreMultiplier += scoreMultiplierIncrease;
            recentOnSpeedChanged = true;
        }
        //ObstacleAI.SpeedChanged -= OnSpeedChanged;
    }

    private float ScoreValue(float baseScore)
    {
        float scoreValue;

        scoreValue = baseScore * scoreMultiplier;

        DebugMessages.SimpleVariableOutput(this, scoreValue, "scoreValue");

        return scoreValue;
    }
}
