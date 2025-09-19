using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float speed = 10; // Set player's movement speed.
    public float rotationSpeed = 20; // Set player's rotation speed.

    public Rigidbody rb; // Reference to player's Rigidbody.
    public GameObject CM; // Center of mass for stable movement
    public List<GameObject> Springs; // Raycast points
    private float hoverStrength = 40; // Amount of force off the ground
    private float hoverDampening = 25; // For stabilizing
    private float lastHitDist = 0; 
    private float length = 15f; // Total distance raycasted
    
    private float HooksLawDampen(float hitDistance)
    {
        float forceAmount = hoverStrength * (length - hitDistance) + (hoverDampening * (lastHitDist - hitDistance));
        forceAmount = Mathf.Max(0f, forceAmount);
        lastHitDist = hitDistance;

        return forceAmount;
    }

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
            // Shoots raycasts to figure out the distance between spring and floor
            RaycastHit hit;
            if (Physics.Raycast(spring.transform.position, transform.TransformDirection(Vector3.down), out hit, length))
            {
                // Applys force according to HooksLawDampen on each spring
                float forceAmount = HooksLawDampen(hit.distance);
                rb.AddForceAtPosition(Time.deltaTime * transform.TransformDirection(Vector3.up) * forceAmount, spring.transform.position);
            }
            else
            {
                lastHitDist = length * 1.1f;
            }
        }
        // Drag I think?
        rb.AddForce(-Time.deltaTime * transform.TransformDirection(Vector3.right) * transform.InverseTransformVector(rb.velocity).x * 5f);
    }
}