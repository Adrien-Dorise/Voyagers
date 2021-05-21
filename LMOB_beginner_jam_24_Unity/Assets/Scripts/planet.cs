using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planet : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject A, B, sprite;
    [SerializeField] private float speed;
    private Vector3 distance;

    void Start()
    {
        speed = 0.15f;
        sprite = transform.GetChild(0).gameObject;
        A = transform.GetChild(1).gameObject;
        B = transform.GetChild(2).gameObject;

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        distance = B.transform.position - sprite.transform.position;
        if(distance.magnitude < 0.5f)
        {
            sprite.transform.position = A.transform.position;
        }
                                                               
        sprite.transform.Translate(distance.normalized * speed * Time.fixedDeltaTime);
       
    }
}
