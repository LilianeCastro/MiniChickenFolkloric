using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstGround : MonoBehaviour
{
    private Rigidbody2D             groundRb;
    private int                     groundId;
    private int                     platformId;
    private float                   posToDestroy;

    private bool                    isInstantiate;

    void Start()
    {
        groundRb = GetComponent<Rigidbody2D>();
        groundId = Random.Range(0,3);
        platformId = Random.Range(3,6);
        posToDestroy = -Mathf.Abs(GameController.Instance.sizeGround) * 2;

    }

    // Update is called once per frame
    void Update()
    {
        groundRb.velocity = new Vector2(GameController.Instance.getSpeed(), 0);

        if(transform.position.x <= GameController.Instance.sizeGround)
        {
            if(transform.position.x <= 0 && !isInstantiate)
            {
                isInstantiate = true;
                GameObject groundTemp = ObjectPoolingManager.Instance.GetPoolObject(groundId);
                groundTemp.transform.position = new Vector2(transform.position.x + GameController.Instance.sizeGround, transform.position.y);
                groundTemp.gameObject.SetActive(true);
            }
        }

        if(transform.position.x < posToDestroy)
        {
            Destroy(this.gameObject);
        }
    }
}
