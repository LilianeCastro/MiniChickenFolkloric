using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    private Rigidbody2D             groundRb;
    private bool                    isInstantiate;

    void Start()
    {
        groundRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        groundRb.velocity = new Vector2(GameController.Instance.getSpeed(), 0);

        if(transform.position.x <= GameController.Instance.sizeGround)
        {
            if(transform.position.x <= 0 && !isInstantiate)
            {
                isInstantiate = true;
                GroundGenerator.Instance.GenGround();
            }
        }
    }

    private void OnBecameInvisible() 
    {
        GroundGenerator.Instance.CoolGround(this.gameObject);
    }
}
