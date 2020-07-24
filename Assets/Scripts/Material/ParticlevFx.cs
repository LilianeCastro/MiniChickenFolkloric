using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlevFx : MonoBehaviour
{
    private void OnParticleCollision(GameObject other) {
        if(other.gameObject.tag=="Enemy")
        {
            Destroy(other.gameObject);
        }
    }
}
