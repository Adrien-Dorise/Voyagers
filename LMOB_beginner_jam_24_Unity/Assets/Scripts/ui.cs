using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui : MonoBehaviour
{
    private Text textObject;
    private GameObject player;
    private float delay;
    // Start is called before the first frame update
    void Start()
    {
        textObject = transform.GetChild(1).gameObject.GetComponent<Text>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        delay = player.GetComponent<player>().getJumpDelayRemainging();
        textObject.text = delay.ToString();

    }
}
