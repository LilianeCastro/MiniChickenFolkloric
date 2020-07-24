using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoSingleton<GameController>
{
    [Serializable]
    public struct Shot{
        public string key;
        public Sprite shotSprite;
    }

    public Shot[] shotRenderer;

    public Sprite[] shotSprite;

    [Header("Game Config")]
    public float speed;

    [Header("Ground Config")]
    public GameObject groundPrefab;
    public float sizeGround;

    [Header("Prefabs")]
    public GameObject platformPrefab;
    public GameObject collectablePrefab;


     public void StartGame()
    {
        Ground.ready = true;
    }

    public void InstantiateObjects(Transform posSpawn, GameObject prefab, int order)
    {
        GameObject platTemp = Instantiate(prefab);
        platTemp.transform.parent = posSpawn.transform;
        platTemp.TryGetComponent(out Renderer rend);
        rend.sortingOrder = order;
        platTemp.transform.localPosition = new Vector2(0, platTemp.transform.position.y);
    }
}
