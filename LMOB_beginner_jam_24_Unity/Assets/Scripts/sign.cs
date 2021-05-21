using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sign : MonoBehaviour
{
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
        sprite = this.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag =="Player")
        {
            sprite.sprite =  Resources.Load<Sprite>("Sign2");
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            sprite.sprite = Resources.Load<Sprite>("Sign1");
            this.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
