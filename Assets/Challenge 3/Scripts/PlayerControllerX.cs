using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;
    private float toptBound = 10.80f;

    public float floatForce = 100.0f;
    public float gravityModifier = 1.5f;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    public AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip bounceSound;
    public AudioClip explodeSound;
    public GameManagerX gameManagerXScript;


    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();

        // Apply a small upward force at the start of the game
        playerRb = GetComponent<Rigidbody>();
        playerRb.AddForce(Vector3.up * floatForce);
        gameManagerXScript = GameObject.Find("GameManagerX").GetComponent<GameManagerX>();

    }

    // Update is called once per frame
    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver && transform.position.y < toptBound)
        {
            playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse );
        }

    }


    private void OnCollisionEnter(Collision other)
    {
        //if player collides with ground bounce
        if(other.gameObject.CompareTag("Ground"))
        {
            playerRb.AddForce(Vector3.up * 18,ForceMode.Impulse);
            playerAudio.PlayOneShot(bounceSound, 1.0f);
        }
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
            StartCoroutine(GameOver());

        }
        if(other.gameObject.CompareTag("Obstacle"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
            StartCoroutine(GameOver());            
        }

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }
    }
    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        gameManagerXScript.gameOver = true;

    }

}
