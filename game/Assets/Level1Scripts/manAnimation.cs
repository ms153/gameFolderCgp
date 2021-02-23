using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manAnimation : MonoBehaviour
{
    Animator anim;  //contains animator object
   
    void Start()
    {
        //moving speed of player (walking or running) determined by
        anim = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        //player jumps when space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space))    
        {
            anim.SetTrigger("Jump");
        }

    }

    //player shoots pistol when E is pressed
    public void ShootGun()                      
    {
        anim.SetTrigger("Shoot");
    }

    //moving speed of player (walking or running) determined by
    //float value passed in from PlayerController script
    public void playerMove(float moveSpeed)     
    {
        anim.SetFloat("Speed", moveSpeed);
    }

    public void Die()
    {
        anim.SetTrigger("Die");
    }
}
