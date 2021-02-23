using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieAnimation : MonoBehaviour
{
    Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }
 
    public void die()
    {
        anim.SetBool("Die", true);  //zombie 'dies'
    }

    public void respawn()
    {
        anim.SetBool("Die", false); //when !Die zombie goes back to walk state
    }

    
}
