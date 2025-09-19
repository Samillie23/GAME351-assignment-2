using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Simple hovercraft controller: WASD/Arrows = move/turn.
/// Framerate-independent (physics is in FixedUpdate with Time.fixedDeltaTime).
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Tunable Stats (per car type)")]
    [Tooltip("Forward max speed in meters/second")]
    [SerializeField] private float maxSpeed = 12f;

    [Tooltip("How fast it turns (degrees/second)")]
    [SerializeField] private float turnRateDegPerSec = 140f;

    [Tooltip("How quickly it reaches target speed (m/s^2)")]
    [SerializeField] private float acceleration = 25f;

    [Tooltip("Extra soft stop when no throttle (0 = none)")]
    [SerializeField] private float softDrag = 1.5f;

    [Header("Options")]
    [SerializeField] private bool lockPitchRoll = true;

    private Rigidbody rb;
    private float inputForward; // -1..1
    private float inputTurn;    // -1..1
    private float currentSpeed; // signed along forward

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        if (lockPitchRoll)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX |
                             RigidbodyConstraints.FreezeRotationZ;
        }
    }

    void Update()
    {
        // Read input in Update (fast & smooth), apply in FixedUpdate.
        inputForward = Input.GetAxisRaw("Vertical");   // W/S or Up/Down
        inputTurn    = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
    }

    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;

        // 1) Forward speed control (accelerate toward target speed)
        float targetSpeed = inputForward * maxSpeed;
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * dt);

        // Build desired velocity along forward (keep gravity/Y from physics)
        Vector3 desired = transform.forward * currentSpeed;
        desired.y = rb.velocity.y;
        rb.velocity = desired;

        // Optional gentle slowdown when no throttle
        if (Mathf.Approximately(inputForward, 0f) && softDrag > 0f)
        {
            Vector3 v = rb.velocity;
            Vector3 lateral = new Vector3(v.x, 0f, v.z);
            lateral *= Mathf.Clamp01(1f - softDrag * dt);
            rb.velocity = new Vector3(lateral.x, v.y, lateral.z);
        }

        // 2) Turning (deg/sec * dt)
        float yaw = inputTurn * turnRateDegPerSec * dt;
        Quaternion turn = Quaternion.Euler(0f, yaw, 0f);
        rb.MoveRotation(rb.rotation * turn);
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        maxSpeed = Mathf.Max(0f, maxSpeed);
        turnRateDegPerSec = Mathf.Max(0f, turnRateDegPerSec);
        acceleration = Mathf.Max(0f, acceleration);
        softDrag = Mathf.Max(0f, softDrag);
    }
#endif
}
