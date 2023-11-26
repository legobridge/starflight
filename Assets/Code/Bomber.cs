using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bomber : MonoBehaviour
{

    //public Transform target; // Assign the target in the inspector

    // Aerodynamic coefficients and movement speeds
    public float ForwardDragCoefficient = 0.01f;
    public float VerticalDragCoefficient = 0.5f;
    public float LiftCoefficient = 0.01f;
    public float PitchRange = 45f;
    public float RollRange = 45;
    public float RotationalSpeed = 5f;
    public float MaximumThrust = 20f;
    public float LerpWeight = 0.01f;

    private Rigidbody npcRB;
    private float yaw, pitch, roll, thrust;
    //private Vector3 windVelocity = new Vector3(0, 0, 0);

    void Start()
    {
        npcRB = GetComponent<Rigidbody>();
        npcRB.velocity = transform.forward * 3;
    }

    

    void FixedUpdate()
    {
        var target = FindObjectOfType<Battleship>();
        Vector3 modifiedTargetPosition = target.transform.position;
        modifiedTargetPosition.y += 70;
        Vector3 localTarget = transform.InverseTransformPoint(modifiedTargetPosition);
   
        // Check if the plane has crossed the target
        bool hasCrossedTarget = localTarget.z < 0;

        if (!hasCrossedTarget)
        {
            // Automated control inputs based on the target's position
            var rollCorrection = Mathf.Atan(localTarget.x / -localTarget.z) * Mathf.Rad2Deg;
            //Debug.Log(rollCorrection);
            float targetRoll = Mathf.Clamp(rollCorrection, -RollRange, RollRange);
            float targetPitch = Mathf.Clamp(-localTarget.y * PitchRange, -PitchRange, PitchRange);
            // TODO: Fly towards sky above battleship
         
            roll = Mathf.Lerp(roll, targetRoll, LerpWeight);
            pitch = Mathf.Lerp(pitch, targetPitch, LerpWeight);

            // Calculate yaw difference and adjust
            float targetYaw = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;
            yaw = Mathf.Lerp(yaw, targetYaw, LerpWeight);
        }
        else
        {
            // Once the target is crossed, keep the plane flying straight ahead
            roll = Mathf.Lerp(roll, 0, LerpWeight);
            pitch = Mathf.Lerp(pitch, 0, LerpWeight);
            yaw = Mathf.Lerp(yaw, transform.eulerAngles.y, LerpWeight); // Maintain current yaw
        }

        npcRB.MoveRotation(Quaternion.Euler(new Vector3(pitch, yaw, roll)));

        // Thrust and aerodynamics as before
        thrust = MaximumThrust;
        Vector3 thrustForce = transform.forward * thrust;

        // Aerodynamic forces
        float vFwd = Vector3.Dot(npcRB.velocity, transform.forward);
        float vUp = Vector3.Dot(npcRB.velocity, transform.up);

        var fwdDragForce = -Mathf.Sign(vFwd) * transform.forward * ForwardDragCoefficient * Mathf.Pow(vFwd, 2);
        var upDragForce = -Mathf.Sign(vUp) * transform.up * VerticalDragCoefficient * Mathf.Pow(vUp, 2);
        var liftForce = transform.up * LiftCoefficient * Mathf.Pow(vFwd, 2);

        npcRB.AddForce(thrustForce + fwdDragForce + upDragForce + liftForce);
    }
}




