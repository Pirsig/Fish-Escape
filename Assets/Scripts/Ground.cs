using System;
using UnityEngine;
using BaseVariables;

public class Ground : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed = 5f;
    [SerializeField]
    private float scrollSpeedIncrease = .05f;
    private int obstaclesPassed;
    [SerializeField]
    private IntReference maxObstaclesPassed;

    private Renderer cachedRenderer;

    private bool playerDead;

    private void Awake()
    {
        cachedRenderer = GetComponent<Renderer>();

        playerDead = false;
        PlayerController.PlayerDied += OnPlayerDied;
        DebugMessages.ClassInObjectSubscribed(this, "PlayerDied");

        ScoreZone.PlayerScored += OnPlayerScored;
        DebugMessages.ClassInObjectSubscribed(this, "PlayerScored");
    }

    private void OnDestroy()
    {
        PlayerController.PlayerDied -= OnPlayerDied;
        DebugMessages.ClassInObjectUnsubscribed(this, "PlayerDied");

        ScoreZone.PlayerScored -= OnPlayerScored;
        DebugMessages.ClassInObjectUnsubscribed(this, "PlayerScored");
    }

    private void OnApplicationQuit()
    {
        PlayerController.PlayerDied -= OnPlayerDied;
        DebugMessages.ClassInObjectUnsubscribed(this, "PlayerDied");

        ScoreZone.PlayerScored -= OnPlayerScored;
        DebugMessages.ClassInObjectSubscribed(this, "PlayerScored");
    }

    void Update()
    {
        if(!playerDead)
        {
            ScrollTexture();
        }
    }

    private void ScrollTexture()
    {
        Vector2 currentTextureOffset = cachedRenderer.material.GetTextureOffset("_MainTex");

        float distanceToScroll = Time.deltaTime * scrollSpeed;

        float newXOffset = currentTextureOffset.x + distanceToScroll;

        Vector2 newOffset = new Vector2(newXOffset, currentTextureOffset.y);

        cachedRenderer.material.SetTextureOffset("_MainTex", newOffset);
    }

    private void OnPlayerDied()
    {
        DebugMessages.EventFired(this, "OnPlayerDied()");
        playerDead = true;
    }

    private void OnPlayerScored()
    {
        DebugMessages.EventFired(this, "OnPlayerScored()");
        obstaclesPassed++;
        if (obstaclesPassed >= maxObstaclesPassed)
        {
            scrollSpeed += scrollSpeedIncrease;
            obstaclesPassed = 0;
        }
    }
}
