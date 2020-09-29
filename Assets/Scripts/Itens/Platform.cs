using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform            posSpawnCollectable;
    private PoolObjectType      poolObjectType;
    private Rigidbody2D         platRb;

    private void Start()
    {
        platRb = GetComponent<Rigidbody2D>();
        
        print(platRb.velocity.x);
    }

    private void Update()
    {
        platRb.velocity = new Vector2(GameController.Instance.getSpeed(), 0);
    }
    public void InstantiateCollectable()
    {
        if(GameController.Instance.CanSpawnAbovePercent(15))
        {
            int idChosen = Random.Range(0, GameController.Instance.collectablePlusPrefab.Length);
            GameController.Instance.instantiateObjects(posSpawnCollectable, GameController.Instance.collectablePlusPrefab, GameController.Instance.collectablePlusPrefab.Length, 2, "");
            
        }
        else if(GameController.Instance.CanSpawnAbovePercent(50))
        {
            int idChosen = Random.Range(0, GameController.Instance.collectablePrefab.Length);
            GameController.Instance.instantiateObjects(posSpawnCollectable, GameController.Instance.collectablePrefab, GameController.Instance.collectablePrefab.Length, 2, "");
        }  
    }

    public void InstantiateCollectableWithEnemy()
    {
        int idChosen = Random.Range(0, GameController.Instance.collectablePlusPrefab.Length);
        GameController.Instance.instantiateObjects(posSpawnCollectable, GameController.Instance.collectablePlusPrefab, GameController.Instance.collectablePlusPrefab.Length, 2, "");
    }

    public void Destroy(PoolObjectType type)
    {
        poolObjectType = type;
        
    }

    private void OnBecameInvisible() {
        ObjectPoolingManager.Instance.CoolObject(this.gameObject, poolObjectType);
    }
}
