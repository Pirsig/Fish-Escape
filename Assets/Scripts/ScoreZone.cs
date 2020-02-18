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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == playerTag)
        {
            score.Value += zoneScoreValue * scoreMultiplier;
            Debug.Log("score = " + score);
        }
    }
}
