using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{


    [SerializeField] GameObject player;
    [SerializeField]  private Vector3 playerPos, myPos, deltaPos;
    [SerializeField] float ratioSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerPos = player.transform.position;
        myPos = this.transform.position;
        ratioSpeed = 0.5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerPos = player.transform.position;
        myPos = this.transform.position;
        deltaPos = playerPos - myPos;
        if(deltaPos.x > 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        transform.Translate(deltaPos * ratioSpeed * Time.fixedDeltaTime);


    }
}
