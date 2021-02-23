using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*-------------------------------------------------------
     * PLAYER MOVEMENT:
     * MOUSE POSITION - Player look around
     *     W/S        - Forwards/backwards walking movement
     *      E         - Shoot weapon
     *  SHIFT KEY     - Sprint     
     *  SPACE KEY     - jump
     * ------------------------------------------------------
    */

    CharacterController controller;
    public GameObject AnimationControl;

    //player movement

    public float verticalInput;

    private float mouseInputX;           //gets input from player mouse movement
    private float mouseInputY;
    private float mouseResponse = 5.0f;  //controls sensitivity of mouse movement
    private float walkSpeed = 2f;       //set character walking speed
    private float runSpeed = 7f;        //set character running speed  
    bool run;                           //set when player is running
    private float moveSpeed;             //determines if move spees is walking or running
    private float jumpSize = 0.5f;       //can jump 0.5 units high
    private float gravity = -10;
    private float velocityY;

    private float lookLimit = 0;         //limits player camera angle in vertical axis
    public Transform cameraLook = null;
    private bool hideMouse = true;       //hides mouse from field of view


    //player shooting
    private int damage = 5;
    //public Transform GunBarrel;
    public GameObject bullet;
   
    //player health
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;


    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        currentHealth = maxHealth;
        
    }

    void Update()
    {

        movePlayer();
        lookAround();
        
        //Checks if player has shot weapon
        if(Input.GetKeyDown(KeyCode.E))
        {
            //accesses shoot function of bullet script
            bullet.GetComponent<Bullet>().Shoot();
            AnimationControl.GetComponent<manAnimation>().ShootGun();
           
        }

    }

    void movePlayer()
    {
        //gets forwards and backwards movements
        verticalInput = Input.GetAxis("Vertical");

        //determines velocity in the Y axis for when player jumps
        velocityY += Time.deltaTime * gravity;

        //if left shift key is pressed run = true
        run = Input.GetKey(KeyCode.LeftShift);

        //when there is no forwards or backwards input player does not move
        if (verticalInput == 0)
        {
            moveSpeed = 0;
        }
        //if left shift key is pressed player runs
        else if (run)
        {
            moveSpeed = runSpeed;
        }
        else
        {
            //player walks
            moveSpeed = walkSpeed;
        }
        
        //Gives float value from current move speed to animator controller
        //speed > 2 = run, speed < 2 = walk, speed == 0 is idle
        AnimationControl.GetComponent<manAnimation>().playerMove(moveSpeed);

        //player jumps when space bar is pressed
        bool jump = Input.GetKey(KeyCode.Space);

        //ensures player is currently touching the ground when space bar is pressed
        if (jump && controller.isGrounded)
        {
            //determines speed of the jump
            float jumpSpeed = Mathf.Sqrt(-2 * gravity * jumpSize);
            velocityY = jumpSpeed;
        }
        if (controller.isGrounded)
        {
            velocityY = 0;
        }

        //moves player forwards and accounts for jumping
        Vector3 velocity = transform.forward * verticalInput * moveSpeed + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);
        
    }

    //applied to camera (child of player object) 
    void lookAround()
    {
        //takes input from x and y of cursor position
        mouseInputX = Input.GetAxis("Mouse X");
        mouseInputY = Input.GetAxis("Mouse Y");

        //takes value of current y input (multiplied by mouse sensitivity) away
        //from the look limit value (initially set to 0)
        lookLimit -= mouseInputY * mouseResponse;

        //ensures that player can't look further than clamped values in y-axis
        lookLimit = Mathf.Clamp(lookLimit, -90.0f, 60.0f);

        //applies look limits to the x axis
        cameraLook.localEulerAngles = Vector3.right * lookLimit;
        //camera rotation movement in x and y axis
        transform.Rotate(Vector3.up * mouseInputX * mouseResponse);

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
