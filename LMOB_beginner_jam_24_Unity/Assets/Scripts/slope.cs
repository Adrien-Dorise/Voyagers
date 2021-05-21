using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slope : MonoBehaviour
{

    private player playerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<player>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "BottomPlayer" )
        {
            
            //print("Giving Slope Information to player");
            playerScript.slopeName = this.name;

        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
