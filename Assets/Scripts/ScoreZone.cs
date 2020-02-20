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
    
    private ObstacleAI firstObstacleAI;
    private float currentSpeed;

    private void Awake()
    {
        firstObstacleAI = GameObject.Find("Obstacle 1").GetComponent<ObstacleAI>();
        currentSpeed = firstObstacleAI.CurrentSpeed;
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
        DebugMessages.OriginatingEventFired(this, "OnPlayerScored()");
        PlayerScored?.Invoke();
        if (currentSpeed != firstObstacleAI.CurrentSpeed)
        {
            currentSpeed = firstObstacleAI.CurrentSpeed;
            scoreMultiplier += scoreMultiplierIncrease;
        }
    }
}
