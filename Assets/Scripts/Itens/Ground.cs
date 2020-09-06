using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private Rigidbody2D             groundRb;
    private bool                    isInstantiate;
    private bool                    isInstantiatePlatform;

    public Transform                posPlatformA;
    public Transform                posPlatformB;

    public static bool              ready;

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
                int idChosen = Random.Range(0, GameController.Instance.groundPrefab.Length);

                GameObject temp = Instantiate(GameController.Instance.groundPrefab[idChosen]);
                temp.transform.position = new Vector2(transform.position.x + GameController.Instance.sizeGround, transform.position.y);
            }

            if(transform.position.x <= GameController.Instance.sizeGround && !isInstantiatePlatform)
            {
                isInstantiatePlatform = true;

                GameController.Instance.instantiateObjects(posPlatformA, GameController.Instance.platformPrefab, GameController.Instance.platformPrefab.Length, 1);
                float rangePosXPlatB = Random.Range(2f, 3.5f);
                posPlatformB.localPosition = new Vector3(rangePosXPlatB, posPlatformB.localPosition.y, posPlatformB.localPosition.z);
                GameController.Instance.instantiateObjects(posPlatformB, GameController.Instance.platformPrefab, GameController.Instance.platformPrefab.Length, 0);
            }
        }

        if(transform.position.x < -Mathf.Abs(GameController.Instance.sizeGround))
        {
            Destroy(this.gameObject);
        }

    }

}
