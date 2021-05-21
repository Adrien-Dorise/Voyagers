using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cameraScript : MonoBehaviour
{
    private float transitionTime = 1f;
    private bool isTransitioning = false;

    [SerializeField] private CinemachineVirtualCamera camera1;
    [SerializeField] private CinemachineVirtualCamera camera2;
    [SerializeField] private CinemachineVirtualCamera camera3;
    [SerializeField] private CinemachineVirtualCamera camera4;
    [SerializeField] private CinemachineVirtualCamera camera5;


    // Start is called before the first frame update
    void Start()
    {
        camera1.enabled = true;
        camera2.enabled = false;
        camera3.enabled = false;
        camera4.enabled = false;
        camera5.enabled = false;

        if(SceneManager.GetActiveScene().name == "Level3")
        {
            camera1.enabled = false;
            camera2.enabled = true;
            camera3.enabled = false;
            camera4.enabled = false;
            camera5.enabled = false;

        }

    }

    IEnumerator transitioning(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isTransitioning = false;
    }

    public void transition(string transNumber)
    {

        if (!isTransitioning)
        {

            switch (transNumber)
            {

                case "TransPart (1)":
                    if (!camera1.enabled)
                    {
                        camera1.enabled = true;
                        camera2.enabled = false;
                        camera3.enabled = false;
                        camera4.enabled = false;
                        camera5.enabled = false;

                        isTransitioning = true;
                        StartCoroutine(transitioning(transitionTime));
                    }
                    break;

                case "TransPart (2)":
                    if (!camera2.enabled)
                    {
                        camera1.enabled = false;
                        camera2.enabled = true;
                        camera3.enabled = false;
                        camera4.enabled = false;
                        camera5.enabled = false;

                        isTransitioning = true;
                        StartCoroutine(transitioning(transitionTime));
                    }

                    break;

                case "TransPart (3)":
                    if (!camera3.enabled)
                    {
                        camera1.enabled = false;
                        camera2.enabled = false;
                        camera3.enabled = true;
                        camera4.enabled = false;
                        camera5.enabled = false;

                        isTransitioning = true;
                        StartCoroutine(transitioning(transitionTime));
                    }
                    break;
                
                
                case "TransPart (4)":
                    if (!camera4.enabled)
                    {
                        camera1.enabled = false;
                        camera2.enabled = false;
                        camera3.enabled = false;
                        camera4.enabled = true;
                        camera5.enabled = false;

                        isTransitioning = true;
                        StartCoroutine(transitioning(transitionTime));
                    }
                    break;
                
                
                case "TransPart (5)":
                    if (!camera4.enabled)
                    {
                        camera1.enabled = false;
                        camera2.enabled = false;
                        camera3.enabled = false;
                        camera4.enabled = false;
                        camera5.enabled = true;

                        isTransitioning = true;
                        StartCoroutine(transitioning(transitionTime));
                    }
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
