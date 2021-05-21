using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManagerLvl2 : MonoBehaviour
{
    private GameObject player,enemy;
    private playerLvl2 playerScript;
    private Vector3 respawnCoord, respawnEnemyCoord;
    private float timeScene;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        enemy = GameObject.Find("Enemy");
        respawnEnemyCoord = GameObject.Find("respawnEnemy").transform.position;
        playerScript = player.GetComponent<playerLvl2>();

        timeScene = 5f;
        StartCoroutine(scene(timeScene));
    }

    IEnumerator scene(float waitTime)
    {

        Time.timeScale = 0;
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Level3");


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
        player.SetActive(true);
        playerScript.isDead = false;

    }

    private void respawnEnemy()
    {
        enemy.transform.position = respawnEnemyCoord;
    }


    // Update is called once per frame
    void Update()
    {
        if (playerScript.isDead)
        {
            respawn();
        }

    }
}
