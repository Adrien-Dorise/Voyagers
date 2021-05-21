using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class playerLvl2 : MonoBehaviour
{
    private GameManagerLvl2 gameManagerScript;
    private cameraScript cameraScrip;
    [SerializeField] GameObject jumpSoundObject, deathSoundObject, itsATrap;
    [SerializeField] private float speed, acceleration, skiSpeed, skiAcceleration, skiAccMax, skiAccDelay, decelerationSpeed, decelerationDelay;
    [SerializeField] private float playerMass, playerLinearDrag, jumpDelay;
    private float jumpDelayClock, jumpDelayRemaining;
    private int audioSourceNumber;
    private AudioClip[] jumpSound = new AudioClip[2];
    private AudioClip deathSound;
    [SerializeField] private bool isJumpPossible;
    public bool isDead = false, isAcc = false, isDeceleration = false, isMoveLock = false, isLeftMoveBlock = false, isRightMoveBlock = false, isTouchingSlope = false;
    public string slopeName;
    public bool isSkiing = false;

    public int accCount = 0, DeceCount = 0;
    public bool endAcc = false, endDecc = false;

    void Start()
    {
        //Parameters
        speed = 2.5f; //Nominal speed of player
        skiSpeed = speed; // Nominal speed skiing
        skiAcceleration = 0.025f; // Acceleration value when skiing
        skiAccDelay = 0.05f; // Delay between each acceleration upgrade
        skiAccMax = 2.2f; // Max Acceleration
        decelerationSpeed = 0.02f; // Deceleration value after skiing
        decelerationDelay = 0.05f; // Delay between each deceleration upgrade
        playerMass = 0.5f; // Play on gravity 
        playerLinearDrag = 3.10f; // Play on gravity
        jumpDelay = 1f; // Delay between each gravity shifting
        jumpDelayClock = 0.1f; // Update time of UI clock

        //Init
        acceleration = 1;
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerLvl2>();
        cameraScrip = GameObject.Find("Cameras").GetComponent<cameraScript>();
        isJumpPossible = true;
        jumpSound[0] = Resources.Load<AudioClip>("Gravity_Shift1");
        jumpSound[1] = Resources.Load<AudioClip>("Gravity_Shift2");
        deathSound = Resources.Load<AudioClip>("Death");
        audioSourceNumber = 0;
        jumpSoundObject.GetComponent<AudioSource>().clip = jumpSound[audioSourceNumber];
        deathSoundObject.GetComponent<AudioSource>().clip = deathSound;
        //Gravity coefficients
        GetComponent<Rigidbody2D>().mass = playerMass;
        GetComponent<Rigidbody2D>().drag = playerLinearDrag;
    }


    //Gravity shifting mechanic
    IEnumerator waitJump(float waitTime)
    {
        isJumpPossible = false;
        while (jumpDelayRemaining > 0f)
        {
            jumpDelayRemaining -= jumpDelayClock;
            yield return new WaitForSeconds(waitTime);
        }
        jumpDelayRemaining = 0.0f;
        isJumpPossible = true;
    }

    //Return Jump delay for UI
    public float getJumpDelayRemainging()
    {
        return (float)Math.Round(jumpDelayRemaining, 2);
    }

    //Collision with spikes/enemy/Slopes
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "Spikes":
                //print("SPIKES DEATH");
                death();
                break;

            case "Enemy":
                //print("ENEMY DEATH");
                death();
                break;

            case "Soul":
                //print("Touching soul");
                SceneManager.LoadScene("Level2");
                break;
        }


    }

    //Checkpoint update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Respawn":
                //print("NEW CHECKPOINT");
                gameManagerScript.updateRespawn(collision.transform.position);
                break;

            case "it's a trap":
                print("NEW CHECKPOINT");

                itsATrap.GetComponent<BoxCollider2D>().enabled = false;
                itsATrap.GetComponent<AudioSource>().Play();
                break;

        }


    }


    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "cameraBoxes")
        {
            //print("Transition " + collision.name);
            cameraScrip.transition(collision.name);
        }


        if (collision.tag == "TriggerSlope")
        {
            isTouchingSlope = true;
        }
    }


    //SKI PART
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "TriggerSlope")
        {
            isTouchingSlope = false;
            //print("SKI Finished");
            if (!isDead && !isDeceleration)
            {
                StartCoroutine(skiDeceleration(decelerationDelay));
            }
        }

    }

    private void ski()
    {
        //print(slopeName);
        switch (slopeName)
        {
            case "SlopeLeftLow":
                if (GetComponent<Rigidbody2D>().gravityScale == 1)
                {
                    this.transform.GetComponentInChildren<SpriteRenderer>().flipX = true;
                    if (!isDead && !isAcc)
                    {
                        StartCoroutine(skiAccelerationFunc(skiAccDelay));
                    }
                    transform.Translate(new Vector3(-1, -1, 0) * skiSpeed * Time.fixedDeltaTime * acceleration);
                }
                else
                {

                    if (!isDead && !isDeceleration)
                    {
                        StartCoroutine(skiDeceleration(decelerationDelay));
                    }

                }
                break;

            case "SlopeLeftHigh":
                if (GetComponent<Rigidbody2D>().gravityScale == -1)
                {

                    this.transform.GetComponentInChildren<SpriteRenderer>().flipX = true;
                    if (!isDead && !isAcc)
                    {
                        StartCoroutine(skiAccelerationFunc(skiAccDelay));
                    }
                    transform.Translate(new Vector3(-1, 1, 0) * skiSpeed * Time.fixedDeltaTime * acceleration);
                }
                else
                {

                    if (!isDead && !isDeceleration)
                    {
                        StartCoroutine(skiDeceleration(decelerationDelay));
                    }
                }
                break;

            case "SlopeRightLow":
                if (GetComponent<Rigidbody2D>().gravityScale == 1)
                {

                    this.transform.GetComponentInChildren<SpriteRenderer>().flipX = false;
                    if (!isDead && !isAcc)
                    {
                        StartCoroutine(skiAccelerationFunc(skiAccDelay));
                    }
                    transform.Translate(new Vector3(1, -1, 0) * skiSpeed * Time.fixedDeltaTime * acceleration);
                }
                else
                {

                    if (!isDead && !isDeceleration)
                    {
                        StartCoroutine(skiDeceleration(decelerationDelay));
                    }
                }
                break;


            case "SlopeRightHigh":
                if (GetComponent<Rigidbody2D>().gravityScale == -1)
                {

                    this.transform.GetComponentInChildren<SpriteRenderer>().flipX = false;
                    if (!isDead && !isAcc)
                    {
                        StartCoroutine(skiAccelerationFunc(skiAccDelay));
                    }
                    transform.Translate(new Vector3(1, 1, 0) * skiSpeed * Time.fixedDeltaTime * acceleration);
                }
                else
                {

                    if (!isDead && !isDeceleration)
                    {
                        StartCoroutine(skiDeceleration(decelerationDelay));
                    }
                }
                break;
        }
    }


    IEnumerator skiAccelerationFunc(float waitTime)
    {
        accCount += 1;
        endAcc = false;


        isAcc = true;
        isDeceleration = false;

        isSkiing = true;
        isMoveLock = true;
        while (acceleration < skiAccMax && isAcc)
        {
            if (!isDeceleration) // Not in the while condition, because if deceleration finishes after, we never go in the loop.
            {
                //print("Acceleration");
                acceleration += skiAcceleration;
            }



            yield return new WaitForSeconds(waitTime);

        }


        endAcc = true;

    }

    IEnumerator skiDeceleration(float waitTime)
    {
        DeceCount += 1;
        endDecc = false;

        isDeceleration = true;
        isAcc = false;

        isMoveLock = false;
        isSkiing = false;
        while (acceleration > 1f && isDeceleration)
        {
            acceleration -= decelerationSpeed;
            if (Input.GetAxis("Horizontal") == 0)
            {
                acceleration = 1f;
            }

            yield return new WaitForSeconds(waitTime);
        }
        if (!isAcc)
        {
            acceleration = 1f;
        }
        isDeceleration = false;


        endDecc = true;

    }


    //Death and respawn

    private void death()
    {
        //print("DEATH");
        deathSoundObject.GetComponent<AudioSource>().clip = deathSound;
        deathSoundObject.GetComponent<AudioSource>().Play();
        isDead = true;
    }

    public void reset()
    {
        isJumpPossible = true;
        isMoveLock = false;
        isSkiing = false;
        isAcc = false;
        isDeceleration = false;
        isLeftMoveBlock = false;
        isRightMoveBlock = false;
        isTouchingSlope = false;
        acceleration = 1f;
        jumpDelayRemaining = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Horizontal movements
        if (!isMoveLock)
        {
            if (Input.GetAxis("Horizontal") > 0 && !isRightMoveBlock)
            {
                if (!isSkiing)
                {
                    this.transform.GetComponentInChildren<SpriteRenderer>().flipX = false;
                }
                transform.Translate(Vector3.right * speed * Time.fixedDeltaTime * Input.GetAxis("Horizontal") * acceleration);
            }
            if (Input.GetAxis("Horizontal") < 0 && !isLeftMoveBlock)
            {
                if (!isSkiing)
                {
                    this.transform.GetComponentInChildren<SpriteRenderer>().flipX = true;
                }
                transform.Translate(Vector3.right * speed * Time.fixedDeltaTime * Input.GetAxis("Horizontal") * acceleration);
            }
        }



        //Shifting gravity
        if (Input.GetAxis("Jump") > 0 && isJumpPossible)
        {
            //print("JUMP DONE");
            jumpDelayRemaining = jumpDelay;
            StartCoroutine(waitJump(jumpDelayClock));
            GetComponent<Rigidbody2D>().gravityScale *= -1;
            this.transform.GetComponentInChildren<SpriteRenderer>().flipY = !this.transform.GetComponentInChildren<SpriteRenderer>().flipY;
            if (audioSourceNumber == 0)
            {
                audioSourceNumber = 1;
                jumpSoundObject.GetComponent<AudioSource>().clip = jumpSound[audioSourceNumber];
            }
            else if (audioSourceNumber == 1)
            {
                audioSourceNumber = 0;
                jumpSoundObject.GetComponent<AudioSource>().clip = jumpSound[audioSourceNumber];
            }
            jumpSoundObject.GetComponent<AudioSource>().Play();

        }


        //ski update
        if (isTouchingSlope)
        {
            ski();
        }







    }







}
