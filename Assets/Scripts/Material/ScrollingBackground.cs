using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    private Renderer meshRenderer;
    private Material currentMaterial;
    private float offset;
    public float increaseOffset;
    public float speed;
    public int orderLayer;
    public string sortingLayer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.sortingLayerName = sortingLayer;
        meshRenderer.sortingOrder = orderLayer;
        currentMaterial = meshRenderer.material;
    }

    void FixedUpdate()
    {
        if(GameManager.Instance.GetStatusPlayer())
        {
            offset += increaseOffset;
            currentMaterial.SetTextureOffset("_MainTex", new Vector2(offset * speed, 0));
        }   
    }
}
