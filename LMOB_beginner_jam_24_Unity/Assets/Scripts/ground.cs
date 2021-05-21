using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ground : MonoBehaviour
{

    private player playerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<player>();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "LeftPlayer")
        {
            //print("LEFT COLLISION");
            playerScript.isLeftMoveBlock = true;
        }
        if (collision.tag == "RightPlayer")
        {
            //print("RIGHT COLLISION");
            playerScript.isRightMoveBlock = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "LeftPlayer")
        {
            
            playerScript.isLeftMoveBlock = false;
        }
        if (collision.tag == "RightPlayer")
        {
            
            playerScript.isRightMoveBlock = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
