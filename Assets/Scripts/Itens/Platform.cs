using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform            posSpawnCollectable;
    private Rigidbody2D         platformRb;
    private int                 idPool;

    private void Start()
    {
        platformRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        platformRb.velocity = new Vector2(GameController.Instance.getSpeed(), 0);
    }

    public void InstantiateCollectable()
    {
        // 3 - 7 no enemies 8 - 16 enemies
        if(GameController.Instance.CanSpawnAbovePercent(85))
        {
            int idChosen = Random.Range(3, 8);
            SpawnCollectable(idChosen);
        }
        else
        {
            int idChosen = Random.Range(8, 17);
            SpawnCollectable(idChosen);
        }  
    }

    public void InstantiateCollectableWithEnemy()
    {
        int idChosen = Random.Range(8, 17);
        SpawnCollectable(idChosen);
    }

    private void SpawnCollectable(int idCollectable)
    {
        print(idCollectable);
        GameObject tempCollectable = ObjectPoolingManager.Instance.GetPoolObject(idCollectable);

        tempCollectable.transform.SetPositionAndRotation(posSpawnCollectable.position, posSpawnCollectable.rotation);
        tempCollectable.transform.parent = posSpawnCollectable.transform;

        tempCollectable.gameObject.SetActive(true);

        tempCollectable.TryGetComponent(out CollectableContainer collectableContainerScript);
        collectableContainerScript.SetInfo(idCollectable);
    }

    public void SetInfo(int idPoolInfo)
    {
        this.idPool = idPoolInfo;   
    }

    private void OnBecameInvisible() 
    {
        ObjectPoolingManager.Instance.CoolObject(this.gameObject, idPool);
    }
}
