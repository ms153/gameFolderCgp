using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwordDamage : MonoBehaviour
{
    public int maxHealth = 20;
    public int currentHealth;
    NavMeshAgent navAgent;
    public GameObject skeletonAnimationControl;
    public Transform spawnPoint;    //respawns zombies at designated Waypoint
    public Transform gameManager;

    public void Start()
    {
        currentHealth = maxHealth;
        navAgent = GetComponent<NavMeshAgent>();
    }
    public void DamageTaken(int damage)
    {
        currentHealth -= damage;


        if (currentHealth <= 0)
        {
            //stops zombie from moving
            navAgent.isStopped = true;

            //updates score 
            gameManager.GetComponent<GameManage>().ScoreUpdate();

            Invoke("RespawnSkeleton", 1);     //waits 1 second
            RespawnSkeleton();
        }


    }

    public void RespawnSkeleton()
    {
        //transports zombie to respawn location -> designated waypoint
        gameObject.transform.position = spawnPoint.position;

        //zombie is respawned with max health
        currentHealth = maxHealth;

        //resumes walking and walk animation
        navAgent.isStopped = false;
        
    }

}
