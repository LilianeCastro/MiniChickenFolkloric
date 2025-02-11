﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Rigidbody2D             bombRb;

    public characterSpecialAttack   charName;

    public float                    force;
    public float                    lifeTime;

    void Start()
    {
        bombRb = GetComponent<Rigidbody2D>();

        switch(charName)
        {
            case characterSpecialAttack.Boto:
                bombRb.velocity = new Vector2(force, force*2);
                break;
            case characterSpecialAttack.Iara:
                bombRb.velocity = new Vector2(force, force);
                break;
            case characterSpecialAttack.Saci:
                bombRb.velocity = new Vector2(force, 0);
                break;
            case characterSpecialAttack.Vitoria:
                bombRb.velocity = new Vector2(force, force*2);
                break;
            default:
                bombRb.AddForce(Vector2.one * force, ForceMode2D.Impulse);
                break;
        }


    }

    private void OnEnable() {
        Invoke("Explosion", lifeTime);
    }

    private void Explosion()
    {
        Instantiate(GameController.Instance.vFxExplosionPrefab[Player.Instance.GetLayerSkin()], transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

}
