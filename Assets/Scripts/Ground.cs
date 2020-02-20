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
        Debug.Log("Ground in " + gameObject.name + " is subscribed to PlayerDied");
    }

    private void OnDestroy()
    {
        PlayerController.PlayerDied -= OnPlayerDied;
        Debug.Log("Ground in " + gameObject.name + " is unsubscribed to PlayerDied");
    }

    private void OnApplicationQuit()
    {
        PlayerController.PlayerDied -= OnPlayerDied;
        Debug.Log("Ground in " + gameObject.name + "is unsubscribed to PlayerDied");
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
        Debug.Log("OnPlayerDied() in " + gameObject.name + " has fired.");
        playerDead = true;
    }
}
