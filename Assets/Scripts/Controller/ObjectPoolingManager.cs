using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolObjectType { GroundNoPlatform, GroundOnePlatform, GroundTwoPlatform }

[Serializable]
public class PoolInfo
{
    public PoolObjectType type;
    public int amount = 0;
    public GameObject prefab;

    [HideInInspector]
    public List<GameObject> pool = new List<GameObject>();
}

public class ObjectPoolingManager : MonoSingleton<ObjectPoolingManager>
{
    
    
    [SerializeField]
    private List<PoolInfo> listOfPool;

    public override void Init()
    {
        base.Init();
        for(int i = 0; i < listOfPool.Count; i++)
        {
            FillPool(listOfPool[i]);
        }
    }

    void FillPool(PoolInfo info)
    {
        for(int i = 0; i < info.amount; i++)
        {
            GameObject obj = Instantiate(info.prefab);
            obj.gameObject.SetActive(false);
            info.pool.Add(obj);
        }
    }

    public GameObject GetPoolObject(PoolObjectType type)
    {
        PoolInfo selected = GetPoolByType(type);
        List<GameObject> pool = selected.pool;

        GameObject obj = null;
        if(pool.Count > 0)
        {
            obj = pool[pool.Count - 1];
            pool.Remove(obj);
        }
        else
        { 
            obj = Instantiate(selected.prefab);
        }
        return obj;
    }

    public void CoolObject(GameObject obj, PoolObjectType type)
    {
        obj.SetActive(false);

        PoolInfo selected = GetPoolByType(type);
        List<GameObject> pool = selected.pool;

        if(!pool.Contains(obj))
        {
            pool.Add(obj);
        }
    }

    private PoolInfo GetPoolByType(PoolObjectType type)
    {
        print(listOfPool[0].prefab);
        for (int i = 0; i < listOfPool.Count; i++)
        {
            if(type.Equals(listOfPool[i].type))
            {
                return listOfPool[i];
            }
        }
        return null;
    }
}
