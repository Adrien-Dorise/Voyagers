using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation : MonoBehaviour
{

    private GameObject player;
    private Rigidbody2D rigidBody;
    private Animator anim;




    // Start is called before the first frame update
    void Start()
    {
        player = this.transform.parent.gameObject;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Ground")
        {
            anim.SetBool("isGrounded", true);
        }


        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            anim.SetBool("isGrounded", false);
        }



    }
    void Update()
    {
        //Walk
        anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        anim.SetBool("isSkiing", player.GetComponent<player>().isSkiing);

    }
}
