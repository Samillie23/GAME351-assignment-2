using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] switchCamaera;
    private CinemachineVirtualCamera cam;
    private int currentCamera;
    private GameObject[] hovercrafts;
    private int current = 0;

    // Start is called before the first frame update
    void Start()
    {
        hovercrafts = GameObject.FindGameObjectsWithTag("Hovercraft");
        current = 0;
        for (int i = 0; i < hovercrafts.Length; i++)
            hovercrafts[i].GetComponent<PlayerController>().enabled = (i == 0);
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
            current = (current + 1) % hovercrafts.Length;
            hovercrafts[current].GetComponent<PlayerController>().enabled = true;

            // Changing active camera
            switchCamaera[currentCamera].Priority = 10;
            currentCamera += 1;
            bool reset = currentCamera == switchCamaera.Length;
            if (reset)
            {
                currentCamera = 0;
            }
            Debug.Log(currentCamera);
            switchCamaera[currentCamera].Priority = 11;
        }
    }
}
