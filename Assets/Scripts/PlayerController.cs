using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float speed = 0; // Set player's movement speed.
    public float rotationSpeed = 0; // Set player's rotation speed.

    public Rigidbody rb; // Reference to player's Rigidbody.
    public GameObject CM; // Center of mass for stable movement
    public List<GameObject> Springs; // Makes the car hover
    public float hoverForce = 0;

    // Start is called before the first frame update
    private void Start()
    {
        rb.centerOfMass = CM.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Handle physics-based movement and rotation.
    private void FixedUpdate()
    {
        // Moving Forward
        rb.AddForce(Time.deltaTime * transform.TransformDirection(Vector3.forward) * Input.GetAxis("Vertical") * (100f * speed));

        // Rotation
        rb.AddTorque(Time.deltaTime * transform.TransformDirection(Vector3.up) * Input.GetAxis("Horizontal") * (100f * rotationSpeed));

        // Hover code
        foreach (GameObject spring in Springs)
        {
            RaycastHit hit;
            if (Physics.Raycast(spring.transform.position, transform.TransformDirection(Vector3.down), out hit, 15f))
            {
                rb.AddForceAtPosition(Time.deltaTime * transform.TransformDirection(Vector3.up) * math.pow(15f - hit.distance, 2) / 15f * (100f * hoverForce), spring.transform.position);
            }
        }
        rb.AddForce(-Time.deltaTime * transform.TransformDirection(Vector3.right) * transform.InverseTransformVector(rb.velocity).x * 5f);
    }
}