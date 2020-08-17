using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {

        if(other.tag=="Shot")
        {
            Destroy(this.gameObject);
        }

    }
}
