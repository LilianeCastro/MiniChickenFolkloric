using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMove : MonoBehaviour
{
    public Transform                posSpawnCollectable;
    
    private Rigidbody2D             groundRb;
    public float                    difX;
    public float                    minX;

    private float                   posX;
    private float                   posY = 0.23f;
    
    void Start()
    {
        groundRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        groundRb.velocity = new Vector2(GameController.Instance.getSpeed(), 0);

        if(transform.position.x <= minX)
        {
            transform.position = new Vector3(transform.position.x + difX * 2,
            transform.position.y,
            transform.position.z);
            SpawnCollectable();
        }
    }

    private void SpawnCollectable()
    {
        int idCollectable = Random.Range(3, 17);

        GameObject tempCollectable = ObjectPoolingManager.Instance.GetPoolObject(idCollectable);

        posX = Random.Range(0f, 2f);

        Vector2 posToSpawn = new Vector2(transform.position.x + posX, transform.position.y + posY);
        tempCollectable.transform.SetPositionAndRotation(posToSpawn, transform.rotation);
        tempCollectable.transform.parent = transform;

        tempCollectable.gameObject.SetActive(true);

        tempCollectable.TryGetComponent(out CollectableContainer collectableContainerScript);
        collectableContainerScript.SetInfo(idCollectable);
    }
}
