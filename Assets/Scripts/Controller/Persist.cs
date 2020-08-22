using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persist : MonoSingleton<Persist>
{
    public override void Init()
    {
        base.Init();

        DontDestroyOnLoad(this.gameObject);

    }
}
