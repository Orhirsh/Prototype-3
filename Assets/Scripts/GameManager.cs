using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float score;
    private PlayerController playerControllerScript;
    public Transform startingPoint; 
    public float lerpSpeed;
    public GameObject restartPanel;

    // Start is called before the first frame update
    void Start()
    {   
  
        //connecting to playercontroller script
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        score = 0;

        //setting game over to control intro animation
        playerControllerScript.gameOver = true;
        //playing intro animation
        StartCoroutine(PlayIntro());
        

    }

    // Update is called once per frame
    void Update()
    {
        //Setting score system
        if(!playerControllerScript.gameOver)
        {
            if(playerControllerScript.dashAbility)
            {
                score += 2;
            }
            else
            {
                score += 1;
            }
        }
        Debug.Log("Score: " + score);
        if (playerControllerScript.restartOption == true)
        {
            restartPanel.SetActive(true);
        }
    }
    //intro animation function
    IEnumerator PlayIntro()
    {
        Vector3 startPos = playerControllerScript.transform.position; //start position
        Vector3 endPos = startingPoint.position;                     //end position
        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;

        float distanceCovered = (Time.time - startTime) * lerpSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;

        playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 0.5f);

        while(fractionOfJourney <1)
        {
            distanceCovered = (Time.time - startTime) * lerpSpeed;
            fractionOfJourney = distanceCovered / journeyLength;
            playerControllerScript.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            yield return null;
        }
        playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 1.0f);
        playerControllerScript.gameOver = false;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
