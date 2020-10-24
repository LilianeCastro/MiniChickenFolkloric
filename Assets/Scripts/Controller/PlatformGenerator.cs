using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    private int idPlatform;

    private float posPlatBX;
    private float posPlatBY = 1f;

    public float minTimeSpawn;
    public float maxTimeSpawn;

    void Start()
    {
        StartCoroutine("GenPlatform");
    }

    IEnumerator GenPlatform()
    {
        
        yield return new WaitForSeconds(Random.Range(minTimeSpawn, maxTimeSpawn));

        if(GameController.Instance.CanSpawnAbovePercent(50))
        {
            SpawnPlatform(transform.position, 0, 25);

            posPlatBX = Random.Range(2f, 2.5f);

            SpawnPlatform(new Vector2(transform.position.x + posPlatBX, transform.position.y + posPlatBY), -1, 100);
        }
        else
        {
            SpawnPlatform(transform.position, 0, 100);
        }

        StartCoroutine("GenPlatform");
    }

    void SpawnPlatform(Vector2 positionToSpawn, int order, int collectableSpawnChange)
    {
        idPlatform = Random.Range(0, 3);

        GameObject tempPlat = ObjectPoolingManager.Instance.GetPoolObject(idPlatform);
        tempPlat.transform.SetPositionAndRotation(new Vector2(positionToSpawn.x, positionToSpawn.y), transform.rotation);

        tempPlat.gameObject.SetActive(true);

        tempPlat.TryGetComponent(out Renderer rend);
        rend.sortingOrder = order;

        tempPlat.TryGetComponent(out Platform platformScript);
        platformScript.SetInfo(idPlatform);

        if(GameController.Instance.CanSpawnAbovePercent(collectableSpawnChange))
        {
            if(order == 0)
            {
                if(GameController.Instance.CanSpawnAbovePercent(70))
                {
                    platformScript.InstantiateCollectableWithEnemy();
                }
                else
                {
                    platformScript.InstantiateCollectable();
                }  
            }
            else
            {
                platformScript.InstantiateCollectable();
            }
        }   
    }
}
