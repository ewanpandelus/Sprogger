using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Innes
//Camera orbits player then lerps towards FPS position when any key is pressed
public class CameraOrbitPlayer : MonoBehaviour
{
    GameObject camera, jar;
    private Controller playerController;
    [SerializeField] float distanceToPlayer = 5f, rotationRate = 15f, zoomTime = 2f;
    bool rotate = true;
    bool keyPressed = false;
    Vector3 startingCameraPos;
    Quaternion startingCameraRot;
    Reticle reticle;
    
    private void Awake()
    {
        camera = transform.GetChild(0).gameObject;
        playerController = GameObject.Find("Player").GetComponent<Controller>();
        jar = GameObject.Find("jar");
        reticle = GameObject.Find("Reticle").GetComponent<Reticle>();
    }

    private void Start()
    {


        startingCameraPos = camera.transform.localPosition;
        startingCameraRot = camera.transform.localRotation;
        MoveCamera();
        

        
    }

    private void Update()
    {
        
        if (rotate)
        {
            RotateCamera();
        }

        if (Input.anyKey)
        {
            keyPressed = true;
            rotate = false;
        }

        if (keyPressed)
        {
            MoveCameraToOriginal();
        }
        
    }


    void MoveCamera()
    {

        camera.transform.localPosition = camera.transform.localPosition + new Vector3(0, distanceToPlayer * 0.3f, -distanceToPlayer);
        camera.transform.LookAt(transform);
    }

    void DisableMouseLook()
    {
        camera.GetComponent<SmoothMouseLook>().enabled = false;
    }

    void EnableMouseLook()
    {
        camera.GetComponent<MouseLookNoSmooth>().enabled = true;
    }

    void RotateCamera()
    {
        transform.Rotate(new Vector3(0, rotationRate, 0) * Time.deltaTime);
    }



    bool tweenHasRun = false;

    void MoveCameraToOriginal()
    {
        if (!tweenHasRun)
        {
            LeanTween.moveLocal(camera, startingCameraPos, zoomTime).setEaseInCubic();
            LeanTween.rotateLocal(gameObject, Vector3.zero, zoomTime).setEaseInCubic();
            LeanTween.rotateLocal(camera, Vector3.zero, zoomTime).setEaseInCubic().setOnComplete(StartGame);
            GameObject[] titles = new GameObject[2];
            titles = GameObject.FindGameObjectsWithTag("Title");
            titles[0].GetComponent<TitleBehaviour>().FadeOut();
            titles[1].GetComponent<TitleBehaviour>().FadeOut();
            GameObject.Find("Audio Whoosh").GetComponent<AudioSource>().Play();
            
        }
        
        tweenHasRun = true;
    }

    void StartGame()
    {
        jar.SetActive(false);
        EnableMouseLook();
        playerController.SetGameStarted();
        reticle.FadeInUI();
        GameObject.Find("WASDPrompt").GetComponent<Prompt>().ShowUI();

        Destroy(this);
    }
}
