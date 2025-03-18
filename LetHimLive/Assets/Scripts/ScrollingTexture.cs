using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingTexture : MonoBehaviour
{
    public Vector2 scrollSpeed = new Vector2(0.5f, 0.5f);
    private Renderer rend;
    private Vector2 currentOffset;

    void Start()
    {
        rend = GetComponent<Renderer>();
        currentOffset = rend.material.mainTextureOffset;
    }

    void Update()
    {
        currentOffset += scrollSpeed * Time.deltaTime;
        rend.material.mainTextureOffset = currentOffset;
    }
}
