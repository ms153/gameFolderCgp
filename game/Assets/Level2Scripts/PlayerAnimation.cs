using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;  //contains animator object

    void Start()
    {
        //moving speed of player (walking or running) determined by
        anim = GetComponent<Animator>();
    }

    
    public void Idle()
    {
        anim.SetBool("Walk", false);
    }


    //player attacks when left mouse button is clicked
    public void Attack()
    {
        anim.SetTrigger("Attack");
    }

    //moving speed of player (walking or running) determined by
    //float value passed in from PlayerController script
    public void Walk()
    {
        anim.SetBool("Walk", true);
    }

}
