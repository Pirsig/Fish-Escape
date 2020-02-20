using UnityEngine;

public class GameOverDisplay : MonoBehaviour
{
    [SerializeField]
    private bool showOnPlayerDied;

    private void Awake()
    {
        //display = this.gameObject;
        PlayerController.PlayerDied += OnPlayerDied;
        Debug.Log("GameOverDisplay in " + gameObject.name + " is subscribed to PlayerDied");
        gameObject.SetActive(!showOnPlayerDied);
    }

    private void OnDestroy()
    {
        PlayerController.PlayerDied -= OnPlayerDied;
        Debug.Log("GameOverDisplay in " + gameObject.name + " is unsubscribed to PlayerDied");
    }

    private void OnApplicationQuit()
    {
        PlayerController.PlayerDied -= OnPlayerDied;
        Debug.Log("GameOverDisplay in " + gameObject.name + "is unsubscribed to PlayerDied");
    }

    public void OnPlayerDied()
    {
        Debug.Log("OnPlayerDied() in " + gameObject.name + " has fired.");
        gameObject.SetActive(showOnPlayerDied);
    }
}
