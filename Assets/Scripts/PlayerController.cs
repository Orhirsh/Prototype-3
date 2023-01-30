using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    public float jumpForce = 10;
    public float gravityModifier;
    public bool isOnGround = true;
    private Animator playerAnim;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public AudioSource playerAudio;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public bool gameOver = false;
    public float doubleJumpForce = 5.0f;
    public bool doubleJumpedUsed = false;
    public bool dashAbility = false;

    
    // Start is called before the first frame update
    void Start()
    {
        //connect to animator
        playerAnim = GetComponent<Animator>();

        //connecting to players ridgid body
        playerRB = GetComponent<Rigidbody>();

        //creating jump variable
        Physics.gravity *= gravityModifier;

        //connecting audio listener
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //setting fast run ability
        if(Input.GetKey(KeyCode.RightArrow))
        {
            dashAbility = true;
            playerAnim.SetFloat("Speed_Multiplier", 2.0f);
            
        }
        else if(dashAbility)
        {
            dashAbility = false;
            playerAnim.SetFloat("Speed_Multiplier", 1.0f);
        }

        
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        
        {
            //applying force to create jump 
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            //changing bool to false to avoid multiple jump while in the air
            isOnGround =false;
   
             //animator jump
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            //change jumped
            doubleJumpedUsed = false; 

        }else if((Input.GetKeyDown(KeyCode.Space) && !isOnGround && !gameOver && !doubleJumpedUsed))
        {
            //2nd jump
            doubleJumpedUsed = true; 
            playerRB.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            playerAnim.Play("Running_Jump", 3, 0f);
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }
    //setting bool of on ground to true once player hit the ground
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground") && !gameOver)
        {
            isOnGround = true;
            dirtParticle.Play();
        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            //setting death animation on impact
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            

            //setting game over state 
            gameOver = true;
            Debug.Log("Game Over");
            
        }
    }

}
