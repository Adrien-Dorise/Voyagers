using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    
    [SerializeField] GameObject enemy, enemy2, canvas2, soul2;
    private GameObject player;
    private player playerScript;
    private Vector3 respawnCoord, respawnEnemyCoord;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
       // enemy = GameObject.Find("Enemy");
        respawnEnemyCoord = GameObject.Find("respawnEnemy").transform.position;
        playerScript = player.GetComponent<player>();
        StartCoroutine(Intro(6f));
    }

    IEnumerator Intro(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        enemy2.SetActive(false);
        canvas2.SetActive(false);
        soul2.SetActive(false);
    }

    public void updateRespawn(Vector3 newCoord)
    {
        respawnCoord = newCoord;
    }

    private void respawn()
    {
        player.SetActive(false);
        player.GetComponent<Rigidbody2D>().gravityScale = 1;
        player.transform.GetComponentInChildren<SpriteRenderer>().flipY = false;
        player.transform.position = respawnCoord;
        playerScript.reset();
        respawnEnemy();
        player.SetActive(true);
        playerScript.isDead = false;

    }

    private void respawnEnemy()
    {
        if (enemy.activeSelf)
        {
            enemy.transform.position = respawnEnemyCoord;
        }
    }


    // Update is called once per frame
    void Update()
    {

        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (playerScript.isDead)
        {
            respawn();
        }

    }
}
