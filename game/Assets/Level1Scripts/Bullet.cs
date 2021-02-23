using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //player shooting
    public Transform GunBarrel;
    public GameObject bullet;
    public GameObject enemy;
    //public Transform bullet;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public Transform attackPoint;
    public float bulletSpeed = 8.0f;
    int Damage = 5;
    public float speed = 500f;
    

    // Update is called once per frame
    void Update()
    {
            //destroys bullet clones after 2 seconds
            if (gameObject.name == "Bullet(Clone)")
            {
                Destroy(gameObject, 2);
            }
      
    }

    public void Shoot()
    {
        //instantiates bullet in position of the player gun barrel
        GameObject bulletShot = Instantiate(bullet, GunBarrel.position, GunBarrel.rotation);

        //bullet has rigid body attached - use this to add force of the forward transform of 
        //gun barrel multiplied by 700
        bulletShot.GetComponent<Rigidbody>().AddForce(GunBarrel.transform.forward * 700);

        
    }


   void OnTriggerEnter (Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //accesses shoot damage class and gives 5 damage
            ShootDamage health = collision.GetComponent<ShootDamage>();

            if (health != null)
            {
                health.DamageTaken(Damage);
            }
        }
    }
    
    
    void BulletDestroy()
    {
        if (gameObject.name == "Bullet(Clone)")
        {
            Destroy(gameObject, 2);
        }
    }

    

}
