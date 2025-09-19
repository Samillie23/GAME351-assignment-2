using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSwitching : MonoBehaviour
{
    private GameObject[] hovercrafts;
    private int current = 0;
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
           if (Input.GetKeyDown(KeyCode.C))
        {
            hovercrafts[current].GetComponent<PlayerController>().enabled = false;
            current = (current + 1) % hovercrafts.Length;
            hovercrafts[current].GetComponent<PlayerController>().enabled = true;
        }   
    }
}
