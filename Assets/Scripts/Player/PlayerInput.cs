using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    void OnJump()
    {
        if(GameManager.Instance.GetStatusPlayer())
        {
            Player.Instance.Jump();
        }
    }

    void OnShot()
    {
        if(GameManager.Instance.GetStatusPlayer())
        {
            Player.Instance.Fire();
        }
    }

    void OnBomb()
    {
        if(GameManager.Instance.GetStatusPlayer())
        {
            Player.Instance.Bomb();
        }
    }
}
