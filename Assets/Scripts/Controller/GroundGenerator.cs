using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoSingleton<GroundGenerator>
{
    public Pool                     PoolController;

    private float                   posToDestroy;
    
    private int                     groundId;
    private GameObject              groundTemp;
    
    public override void Init()
    {
        base.Init();

        GenGround();
    }

    public void GenGround()
    {
        groundId = Random.Range(0,3);

        groundTemp = PoolController.GetPoolObject(0);

        groundTemp.transform.SetPositionAndRotation(new Vector2(transform.position.x + 5, transform.position.y), transform.rotation);
        groundTemp.gameObject.SetActive(true);
    }

    public void CoolGround(GameObject ground)
    {
        ground.transform.parent = null;
        PoolController.CoolObject(ground, groundId);
    }
}
