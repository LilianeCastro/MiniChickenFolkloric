﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum characterSpecialAttack{
    Boto, Boi, Curupira, Iara, Saci, Vitoria
}

public class GameController : MonoSingleton<GameController>
{
    public Sprite[] shotSprite;

    [Header("Game Config")]
    public float speed;

    [Header("Ground Config")]
    public GameObject[] groundPrefab;
    public float sizeGround;

    [Header("Prefabs")]
    public GameObject[] platformPrefab;
    public GameObject[] collectablePrefab;

    [Header("Prefab that depends on the skin")]
    public GameObject[] specialAttackPrefab;
    public GameObject[] vFxExplosionPrefab;

    public void StartGame()
    {
        Ground.ready = true;
    }

    public void InstantiateObjects(Transform posSpawn, GameObject[] prefab, int size, int order)
    {
        int idChosen = Random.Range(0, size);

        GameObject platTemp = Instantiate(prefab[idChosen]);
        platTemp.transform.parent = posSpawn.transform;
        platTemp.TryGetComponent(out Renderer rend);
        rend.sortingOrder = order;
        platTemp.transform.localPosition = new Vector2(0, platTemp.transform.position.y);
    }
}
