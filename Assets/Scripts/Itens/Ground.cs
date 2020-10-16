using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private Rigidbody2D             groundRb;
    private bool                    isInstantiate;
    private bool                    isInstantiatePlatform;
    private int                     idChosen;
    private float                   posRangeSpawn;

    public Transform                posPlatformA;
    public Transform                posPlatformB;
    public Transform                posSpawnCollectable;

    private int                     groundId;
    private int                     platformId;
    private float                   posToDestroy;

    void Start()
    {
        groundRb = GetComponent<Rigidbody2D>();

        groundId = Random.Range(0,3);
        platformId = Random.Range(3,6);

        posToDestroy = -Mathf.Abs(GameController.Instance.sizeGround) * 2;
    }

    void Update()
    {
        groundRb.velocity = new Vector2(GameController.Instance.getSpeed(), 0);

        if(transform.position.x <= GameController.Instance.sizeGround)
        {
            if(transform.position.x <= 0 && !isInstantiate)
            {
                isInstantiate = true;
                idChosen = Random.Range(0, GameController.Instance.groundPrefab.Length);

                GameObject temp = Instantiate(GameController.Instance.groundPrefab[idChosen]);
                temp.transform.position = new Vector2(transform.position.x + GameController.Instance.sizeGround, transform.position.y);
                /*GameObject groundTemp = ObjectPoolingManager.Instance.GetPoolObject(groundId);
                groundTemp.transform.position = new Vector2(transform.position.x + GameController.Instance.sizeGround, transform.position.y);
                groundTemp.transform.rotation = transform.rotation;
                groundTemp.gameObject.SetActive(true);*/
            }

            if(transform.position.x <= GameController.Instance.sizeGround && !isInstantiatePlatform)
            {
                isInstantiatePlatform = true;
                //Instantiate Collectable on Ground
                if (GameController.Instance.CanSpawnAbovePercent(70))
                {
                    posRangeSpawn = Random.Range(GameController.Instance.sizeGround, 15);
                    posSpawnCollectable.position = new Vector3(posRangeSpawn, posSpawnCollectable.transform.position.y, transform.position.z);
                    if (GameController.Instance.CanSpawnAbovePercent(40))
                    {
                        GameController.Instance.instantiateObjects(posSpawnCollectable, GameController.Instance.collectablePrefab, GameController.Instance.collectablePrefab.Length, 2, "");
                    }
                    else
                    {
                        GameController.Instance.instantiateObjects(posSpawnCollectable, GameController.Instance.collectablePlusPrefab, GameController.Instance.collectablePrefab.Length, 2, "");
                    }
                    
                }
                
                //Instantiate PlatformA
                GameController.Instance.instantiateObjects(posPlatformA, GameController.Instance.platformPrefab, GameController.Instance.platformPrefab.Length, 1, "CollectablePlatA");
                //GameObject tempPlatA = ObjectPoolingManager.Instance.GetPoolObject(platformId);
                //tempPlatA.TryGetComponent(out Rigidbody2D platRb);
                //platRb.velocity = new Vector2(GameController.Instance.getSpeed(), 0);
                //tempPlatA.transform.SetPositionAndRotation(new Vector2(posPlatformA.position.x + 10, posPlatformA.position.y), posPlatformA.rotation);
                //tempPlatA.transform.parent = posPlatformA.transform;
                //tempPlatA.transform.localPosition = new Vector2(0, transform.position.y + 1f);
                
                //tempPlatA.TryGetComponent(out Platform plat);
                //tempPlatA.gameObject.SetActive(true);
                //plat.SetInfo(platformId);


                //Instantiate PlatformB
                float rangePosXPlatB = Random.Range(2f, 3.5f);
                posPlatformB.localPosition = new Vector3(rangePosXPlatB, posPlatformB.localPosition.y, posPlatformB.localPosition.z);
                GameController.Instance.instantiateObjects(posPlatformB, GameController.Instance.platformPrefab, GameController.Instance.platformPrefab.Length, 0, "CollectablePlatB");
                
            }
        }

        if(transform.position.x < posToDestroy)
        {
            Destroy(this.gameObject);

            //ObjectPoolingManager.Instance.CoolObject(this.gameObject, groundId);
        }

    }

}
