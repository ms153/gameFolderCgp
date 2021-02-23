using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootDamage : MonoBehaviour
{
    public int maxHealth = 20;
    public int currentHealth;
    NavMeshAgent navAgent;
    public GameObject zombieAnimationControl;
    public Transform spawnPoint;    //respawns zombies at designated Waypoint
    public Transform gameManager;

    public void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }
    public void DamageTaken(int damage)
    {
        currentHealth -= damage;
        

        if (currentHealth <= 0)
        {
            //stops zombie from moving
            navAgent.isStopped = true;

            //calls the zombie die function in the zombie animation script
            zombieAnimationControl.GetComponent<zombieAnimation>().die();
            gameManager.GetComponent<SceneManager>().ScoreUpdate();
            //uses invoke to allow zombie 'die' animation to play before respawn
            Invoke("RespawnZombie", 1);     //waits 1 second
        }


    }

    public void RespawnZombie()
    {
        //transports zombie to respawn location -> designated waypoint
        gameObject.transform.position = spawnPoint.position;

        //zombie is respawned with max health
        currentHealth = maxHealth;  

        //resumes walking and walk animation
        navAgent.isStopped = false;
        zombieAnimationControl.GetComponent<zombieAnimation>().respawn();
    }

    
}
