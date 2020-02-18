using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed = 5f;

    private Renderer cachedRenderer;

    private void Awake()
    {
        cachedRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        Vector2 currentTextureOffset = cachedRenderer.material.GetTextureOffset("_MainTex");

        float distanceToScroll = Time.deltaTime * scrollSpeed;

        float newXOffset = currentTextureOffset.x + distanceToScroll;

        Vector2 newOffset = new Vector2(newXOffset, currentTextureOffset.y);

        cachedRenderer.material.SetTextureOffset("_MainTex", newOffset);
    }
}
