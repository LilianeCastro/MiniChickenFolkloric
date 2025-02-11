﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableContainer : MonoBehaviour
{
    private int                 idPool;
    
    public void SetInfo(int idPoolInfo)
    {
        this.idPool = idPoolInfo;

        foreach (Transform child in transform)
        {
            if(!child.gameObject.activeInHierarchy)
            {
                child.gameObject.SetActive(true);
            }   
        }
    }

    private void OnBecameInvisible() 
    {
        Invoke("CoolContainer", 0.5f);
    }

    private void CoolContainer()
    {
        this.gameObject.transform.parent = null;

        ObjectPoolingManager.Instance.CoolObject(this.gameObject, idPool);
    }
}
