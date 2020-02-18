using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverDisplay : MonoBehaviour
{
    [SerializeField]
    private bool showOnPlayerDied;

    private void Awake()
    {
        //display = this.gameObject;
        PlayerController.PlayerDied += OnPlayerDied;
        Debug.Log("GameOverDisplay is subscribed to PlayerDied");
        gameObject.SetActive(!showOnPlayerDied);
    }

    private void OnApplicationQuit()
    {
        PlayerController.PlayerDied -= OnPlayerDied;
        Debug.Log("GameOverDisplay is unsubscribed to PlayerDied");
    }

    public void OnPlayerDied()
    {
        Debug.Log("OnPlayerDied() in GameOverDisplay has fired.");
        gameObject.SetActive(showOnPlayerDied);
    }
}
