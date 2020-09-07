using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEnemy : MonoBehaviour
{
    private void OnBecameInvisible() {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Shot"))
        {
            Destroy(this.gameObject);
        }
    }
}
