﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum characterSpecialAttack{
    Boto, Boi, Curupira, Iara, Saci, Vitoria
}

public class GameController : MonoSingleton<GameController>
{
    public Sprite[] shotSprite;

    [Header("HUD")]
    public Image		    imgAttack;
    public Image		    imgSpecialAttack;
    public Slider		    progressAttack;
    public Slider		    progressSpecialAttack;
    public Text			    txtScore;

    [Header("Game Config")]
    public float		    speed;
    private int             score;

    [Header("Ground Config")]
    public GameObject[]		groundPrefab;
    public float		    sizeGround;

    [Header("Prefabs")]
    public GameObject[]		platformPrefab;
    public GameObject[]		collectablePrefab;

    [Header("Prefab that depends on the skin")]
    public GameObject[]		specialAttackPrefab;
    public GameObject[]		vFxExplosionPrefab;

    private void Start() {
        progressAttack.value = progressAttack.maxValue;
        progressSpecialAttack.value = progressSpecialAttack.maxValue;
    }

    public void startGame()
    {
	    Ground.ready = true;
    }

    private void FixedUpdate() {
        if(Ground.ready)
        {
            progressAttack.value += Time.deltaTime;
            progressSpecialAttack.value += Time.deltaTime;
        }
    }

    public void updateScore(int value)
    {
        score += value;
        txtScore.text = score.ToString();
    }

    public void updateProgressAttack(int value)
    {
        progressAttack.value += value;
    }

    public void updateProgressSpecialAttack(int value)
    {
        progressSpecialAttack.value += value;
    }

    public void instantiateObjects(Transform posSpawn, GameObject[] prefab, int size, int order)
    {
	int idChosen = Random.Range(0, size);

	GameObject platTemp = Instantiate(prefab[idChosen]);
	platTemp.transform.parent = posSpawn.transform;
	platTemp.TryGetComponent(out Renderer rend);
	rend.sortingOrder = order;
	platTemp.transform.localPosition = new Vector2(0, platTemp.transform.position.y);
    }

}
