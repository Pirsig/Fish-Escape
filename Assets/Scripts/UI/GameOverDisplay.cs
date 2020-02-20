using UnityEngine;

public class GameOverDisplay : MonoBehaviour
{
    [SerializeField]
    private bool showOnPlayerDied;

    private void Awake()
    {
        //display = this.gameObject;
        PlayerController.PlayerDied += OnPlayerDied;
        DebugMessages.ClassInObjectSubscribed(this, "PlayerDied");
        gameObject.SetActive(!showOnPlayerDied);
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

    public void OnPlayerDied()
    {
        DebugMessages.EventFired(this, "OnPlayerDied()");
        gameObject.SetActive(showOnPlayerDied);
    }
}
