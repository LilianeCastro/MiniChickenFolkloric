using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PoolInfo
{
    public int                  idPool;
    public int                  amount = 0;
    public GameObject           prefab;

    [HideInInspector]
    public List<GameObject> pool = new List<GameObject>();
}

public class ObjectPoolingManager : MonoSingleton<ObjectPoolingManager>
{
    [SerializeField]
    private List<PoolInfo> listOfPool = null;

    private List<GameObject> poolSelected;
    private PoolInfo selected;
    private GameObject obj;

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
            obj = Instantiate(info.prefab);
            obj.gameObject.SetActive(false);
            info.pool.Add(obj);
        }
    }

    public GameObject GetPoolObject(int idPool)
    {
        selected = GetPoolById(idPool);
        poolSelected = selected.pool;

        obj = null;

        if(poolSelected.Count > 0)
        {
            obj = poolSelected[poolSelected.Count - 1];
            poolSelected.Remove(obj);
        }
        else
        { 
            obj = Instantiate(selected.prefab);
        }
        return obj;
    }

    public void CoolObject(GameObject obj, int idPool)
    {
        obj.SetActive(false);

        selected = GetPoolById(idPool);
        poolSelected = selected.pool;

        if(!poolSelected.Contains(obj))
        {
            poolSelected.Add(obj);
        }
    }

    private PoolInfo GetPoolById(int idPool)
    {
        for (int i = 0; i < listOfPool.Count; i++)
        {
            if(idPool.Equals(listOfPool[i].idPool))
            {
                return listOfPool[i];
            }
        }
        return null;
    }
}
