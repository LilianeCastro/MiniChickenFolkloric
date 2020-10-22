using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMove : MonoBehaviour
{
    private Rigidbody2D             groundRb;
    public float                    difX;
    public float                    minX;
    
    void Start()
    {
        groundRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        groundRb.velocity = new Vector2(GameController.Instance.getSpeed(), 0);

        if(transform.position.x <= minX)
        {
            transform.position = new Vector3(transform.position.x + difX *2,
            transform.position.y,
            transform.position.z);
        }
    }
}
