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

    private void Awake()
    {
        score.Value = 0;
        ObstacleAI.SpeedChanged += OnSpeedChanged;
        DebugMessages.ClassInObjectSubscribed(this, "SpeedChanged");
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
            score.Value += zoneScoreValue * scoreMultiplier;
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
        DebugMessages.EventFired(this, "SpeedChanged");
        scoreMultiplier += scoreMultiplierIncrease;
    }
}
