using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] switchCamaera;
    private CinemachineVirtualCamera cam;
    private int currentCamera;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SwitchCamera();
    }
    
    private void SwitchCamera() 
    {

        if (Input.GetKeyDown(KeyCode.C)) 
        {
           
         switchCamaera[currentCamera].Priority = 10;
         currentCamera+=1;
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
