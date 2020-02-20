using System;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed = 5f;

    private Renderer cachedRenderer;

    private bool playerDead;

    private void Awake()
    {
        cachedRenderer = GetComponent<Renderer>();

        playerDead = false;
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
}
