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
        print(GameManager.Instance.GetIsFirstTime());
        if(GameManager.Instance.GetIsFirstTime()==0)
        {
            print("first");
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
