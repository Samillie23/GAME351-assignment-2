using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] switchCamera;
    private CinemachineVirtualCamera cam;
    private int currentCamera;
    [SerializeField] private GameObject[] hovercrafts;
    private int current;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SwitchCar();
    }

    private void SwitchCar()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Changing active car
            hovercrafts[current].GetComponent<PlayerController>().enabled = false;
            hovercrafts[current].GetComponent<AudioSource>().enabled = false;
            current = (current + 1) % hovercrafts.Length;
            hovercrafts[current].GetComponent<PlayerController>().enabled = true;
            hovercrafts[current].GetComponent<AudioSource>().enabled = true;
            
            // Changing active camera
            switchCamera[currentCamera].Priority = 10;
            currentCamera += 1;
            bool reset = currentCamera == switchCamera.Length;
            if (reset)
            {
                currentCamera = 0;
            }
            switchCamera[currentCamera].Priority = 11;
        }
    }
}
