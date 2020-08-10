using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    private Rigidbody2D             shotRb;

    private void OnEnable() {
        shotRb = GetComponent<Rigidbody2D>();

        shotRb.velocity = new Vector2(GameController.Instance.getSpeedShot(),0);
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable() {
        CancelInvoke();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Enemy")
        {
            Destroy();
        }
    }

    private void OnBecameInvisible() {
        Destroy();
    }
}
