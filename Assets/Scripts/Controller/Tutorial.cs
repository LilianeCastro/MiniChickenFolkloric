using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private Animator    tutorialAnim;

    private void Awake()
    {
        tutorialAnim = GetComponent<Animator>();    
    }

    void Start()
    {
        if(GameManager.Instance.GetIsFirstTime()==0)
        {
            tutorialAnim.SetTrigger("first");
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    void EndTutorial()
    {
        this.gameObject.SetActive(false);
    }

    
}
