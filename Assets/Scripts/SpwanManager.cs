using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanManager : MonoBehaviour
{
    public float startDelay = 2.0f;
    public float repeatRate = 1.5f;
    public GameObject[] obstaclePrefab;
    private PlayerController playerControllerScript;

    private Vector3 spwanPos = new Vector3(25,0,0);

    // Start is called before the first frame update
    void Start()
    {
        //start spwan function
        InvokeRepeating("SpwanObstacle", startDelay, repeatRate);

        //connect to PlayerController Script
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //rendom Spwan obstacle function
    void SpwanObstacle()
    {
        int index = Random.Range(0, obstaclePrefab.Length);
        if(playerControllerScript.gameOver == false)
        {
            Instantiate(obstaclePrefab[index], spwanPos, obstaclePrefab[index].transform.rotation);
        }
        
    }
}
