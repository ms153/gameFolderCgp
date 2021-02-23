using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SkeletonControl : MonoBehaviour
{
    public GameObject skeleton;

    //waypoints
    NavMeshAgent navAgent;
    GameObject[] waypoints;        //creates array of waypoints used to move the enemy around on patrol
    public int presentWaypoint;    //determines current waypoint

    //attack and chase player   
    public Transform player;        //accesses Player position
    private int hitDamage = 10;

    void Start()
    {
        //skeletonAnimationControl.GetComponent<SkeletonAnimation>().Walk();
        navAgent = GetComponent<NavMeshAgent>();
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");
        presentWaypoint = 0;    //tells enemy to go to waypoint 0
        WaypointCheck();        //ensures that zombie starts at closest waypoint

    }

    // Update is called once per frame
    void Update()
    {
        WaypointAdvance();  //checks if enemy can advance to next waypoint

        //if distance less then chase player
        if (Vector3.Distance(player.position, transform.position) < 4.0f)
        {
            PlayerChase();
        }
        else
        {
            //Resume patrolling at waypoints
            navAgent.isStopped = false;
            navAgent.SetDestination(waypoints[presentWaypoint].transform.position);
        }

        

    }

    //damages player if zombie reaches them
    void OnTriggerEnter(Collider collision)
    {
        //add animation reference here
        if (collision.gameObject.tag == "Player")
        { 
            AttackPlayer();
        }
    }

    //repeatedly cycles through all waypoints -> Advances to next waypoint when close enough
    private void WaypointAdvance()
    {
        if (Vector3.Distance(waypoints[presentWaypoint].transform.position, transform.position) < 2.0f)
        {
            presentWaypoint++;
            
            //if next waypoint exceeds total number of waypoints, go back to first waypoint again
            if (presentWaypoint >= waypoints.Length)
            {
                presentWaypoint = 0;
            }

        }
    }

    //if zombie is closer to a waypoint than waypoint 0, make that it's starting destination
    private void WaypointCheck()
    {
        //checks distance from presentWaypoint -> begins at waypoint 0
        var distanceCheck = Vector3.Distance(transform.position, waypoints[presentWaypoint].transform.position);
        for (var i = 1; i < waypoints.Length; i++)
        {
            //checks distance from each of the other waypoints available
            var distanceWaypoint = Vector3.Distance(transform.position, waypoints[i].transform.position);
            //if one of the other waypoints is closer -> make that starting waypoint
            if (distanceWaypoint < distanceCheck)
            {
                presentWaypoint = i;
            }
        }
    }

    private void PlayerChase()
    {
        //changes zombie destination from current waypoint to position of player
        navAgent.SetDestination(player.transform.position);
        
        //checks to see if distance between zombie and player is less than 1.5
        if (Vector3.Distance(player.position, transform.position) < 1.5f)
        {
            navAgent.isStopped = true;

        }
    }

    private void AttackPlayer()
    {
        skeleton.GetComponent<SkeletonAnimation>().Attack();
        //Deals 10 damage to player
        player.GetComponent<PlayerControl>().DamageTaken(hitDamage);
        Debug.Log("Attacking Player");
    }


}
