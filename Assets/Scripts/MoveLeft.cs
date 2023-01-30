using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private PlayerController playerControllerScript;
    public float speed = 25;
    private float leftBound = -15.0f;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //moving background & Obstacle per player's speed and stoping if game over
        if (playerControllerScript.gameOver == false)
        {
            if(playerControllerScript.dashAbility)
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed * 3);
            } 
            else
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
        }
        //destroy objects that the player avoided
        if(transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
