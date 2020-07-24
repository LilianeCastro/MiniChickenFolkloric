using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private Rigidbody2D groundRb;
    private bool isInstantiate;
    private bool isInstantiatePlatform;

    public Transform posPlatformA;
    public Transform posPlatformB;


    public static bool ready;

    void Start()
    {
        groundRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        groundRb.velocity = new Vector2(GameController.Instance.speed, 0);
        if(transform.position.x <= GameController.Instance.sizeGround)
        {
            if(transform.position.x <= 0 && !isInstantiate)
            {
                isInstantiate = true;
                GameObject temp = Instantiate(GameController.Instance.groundPrefab);
                temp.transform.position = new Vector2(transform.position.x + GameController.Instance.sizeGround, transform.position.y);
            }

            if(transform.position.x <= GameController.Instance.sizeGround && !isInstantiatePlatform)
            {
                isInstantiatePlatform = true;

                GameController.Instance.InstantiateObjects(posPlatformA, GameController.Instance.platformPrefab, 1);

                GameController.Instance.InstantiateObjects(posPlatformB, GameController.Instance.platformPrefab, 0);
            }
        }

        if(transform.position.x < GameController.Instance.sizeGround * -1)
        {
            Destroy(this.gameObject);
        }

    }


}
