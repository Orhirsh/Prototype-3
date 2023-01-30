using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;
    // Start is called before the first frame update
    void Start()
    {
        //initiliziong the start position od the background
        startPos = transform.position;
        //getting exact width of the runing background to set repeat
        repeatWidth = GetComponent<BoxCollider>().size.x /2;
    }

    // Update is called once per frame
    void Update()
    {
        //creating reapeat for neverending background feeling
        if(transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
    }

}
