using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    private Renderer    meshRenderer;
    private Material    currentMaterial;
    private float       offset;
    private int         idTex;

    public float        increaseOffset;
    public float        speed;
    public string       sortingLayer;
    public int          orderLayer;
    
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.sortingLayerName = sortingLayer;
        meshRenderer.sortingOrder = orderLayer;
        currentMaterial = meshRenderer.material;
        idTex = Shader.PropertyToID("_MainTex");
    }

    void FixedUpdate()
    {
        if(GameManager.Instance.GetStatusPlayer())
        {
            offset += increaseOffset;
            currentMaterial.SetTextureOffset(idTex, new Vector2(offset * speed, 0));
        }   
    }
}
