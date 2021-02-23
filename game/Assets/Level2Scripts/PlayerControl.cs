using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject AnimationControl;
    

    //player movement
    private Transform playerMoveTo;
    public GameObject moveToPoint;
    float distanceToLocation = 0.0f;
    public bool moving;
    

    //player health
    public HealthBar healthBar;
    public int maxHealth = 100;
    public int currentHealth;

    //player attack
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public Transform strikePoint;
    int Damage = 5;


    // Start is called before the first frame update
    void Start()
    {
        //sets health to full
        currentHealth = maxHealth;

        //sets healthbar to full
        healthBar.maxHealth(maxHealth);

        //instantiates moveToPoint with reference to current player position
        playerMoveTo = Instantiate(moveToPoint.transform, transform.position, Quaternion.identity);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //defines the movement plane as the y-axis
        Plane movePlane = new Plane(Vector3.up, transform.position);

        //line from camera position to mouse position on screen - lets us know where the mouse is
        Ray rayLine = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);

        //when mouse cursor is moved uses Raycast to dermine distance to clicked location
        if (movePlane.Raycast(rayLine, out distanceToLocation))
        {
            //returns point at distance selected by mouse cursor
            Vector3 posMouse = rayLine.GetPoint(distanceToLocation);

            //when mouse is clicked set destination to place determined by raycast
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                moving = true;
                playerMoveTo.transform.position = posMouse;
                AnimationControl.GetComponent<PlayerAnimation>().Walk();
            }


        }

        if (moving)
        {
            Move();
            //makes sure player stops at destination
            if (transform.position == playerMoveTo.transform.position)
            {
                moving = false;
                AnimationControl.GetComponent<PlayerAnimation>().Idle();
            }

        }

        else if (!moving)
        {
            AnimationControl.GetComponent<PlayerAnimation>().Idle();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            moving = false;
            Attack();
        }
        
    }

    public void Move()
    {

        transform.position = Vector3.MoveTowards(transform.position, playerMoveTo.transform.position, 0.05f);
        transform.LookAt(playerMoveTo.transform);
        AnimationControl.GetComponent<PlayerAnimation>().Walk();
    }

    public void Attack()
    {
        moving = false;
        AnimationControl.GetComponent<PlayerAnimation>().Attack();

        //uses collider array so that it can be applies to any enemy with the layer value 'Enemy'
        //overlap sphere is determined by strikePoint object 
        //attack range gives size of attack contact sphere
        Collider[] strikeEnemy = Physics.OverlapSphere(strikePoint.position, attackRange, enemyLayers);

        //for each enemy hit give damage
        foreach (Collider enemy in strikeEnemy)
        {
            enemy.GetComponent<SwordDamage>().DamageTaken(Damage);
            Debug.Log("We hit " + enemy.name);
        }

    }

    //used to determine size and range of strikepoint
    private void OnDrawGizmosSelected()
    {
        if (strikePoint == null)
            return;

        Gizmos.DrawWireSphere(strikePoint.position, attackRange);
    }


    public void DamageTaken(int damage)
    {
        //reduces health by 5
        currentHealth -= damage;

        //displays current health value in health bar at top of screen
        healthBar.healthLevel(currentHealth);

        //player dies
        if (currentHealth <= 0)
        {
            //displays game over and stops play mode of editor
            Debug.LogError("Game Over");
            UnityEditor.EditorApplication.isPlaying = false;

        }
    }


}
