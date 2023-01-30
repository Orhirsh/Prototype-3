using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFalling : SpawnManagerX
{
    private PlayerControllerX playerControllerScript;
    private int spawnDelay = 10;
    private int spawnInterval = 6;
    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;
    // Start is called before the first frame update
    void Start()
    {
        RandomObjects();
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void SpawnObjects()
    {
        // Set random spawn location and random object index
        Vector3 spawnLocation = new Vector3(Random.Range(40, 45), 19, 0);
        int index = Random.Range(0, objectPrefabs.Length);

        // If game is still active, spawn new object
        if (!playerControllerScript.gameOver)
        {
            Instantiate(objectPrefabs[index], spawnLocation, objectPrefabs[index].transform.rotation);
        }
    }
    public override void RandomObjects()
    {
        InvokeRepeating("SpawnObjects", spawnDelay, spawnInterval);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Money"))
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerControllerScript.playerAudio.PlayOneShot(playerControllerScript.explodeSound, 1.0f);
            Destroy(other.gameObject);
            Destroy(gameObject);
            
            
        }
    }
}