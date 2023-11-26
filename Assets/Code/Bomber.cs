using System;
using UnityEngine;
using UnityEngine.UIElements;

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

    // HP
    public int MaxHitPoints = 25;
    public int DamageTakenPerHit = 5;

    // Bombing
    public GameObject BombPrefab;

    private Rigidbody npcRB;
    private Rigidbody battleshipRB;
    private float yaw, pitch, roll, thrust;
    private int _remainingHp;
    private bool _hasBombDropped = false;
    private bool _isCrashing = false;

    void Start()
    {
        npcRB = GetComponent<Rigidbody>();
        npcRB.velocity = transform.forward * 3;
        battleshipRB = FindObjectOfType<Battleship>().gameObject.GetComponent<Rigidbody>();
        _remainingHp = MaxHitPoints;
    }

    private void HandleMovement()
    {
        if (!_isCrashing)
        {
            Vector3 modifiedTargetPosition = battleshipRB.transform.position;
            modifiedTargetPosition.y += 70;
            Vector3 localTarget = transform.InverseTransformPoint(modifiedTargetPosition);

            if (!_hasBombDropped)
            {
                // If target hasn't been bombed yet, fly towards it
                var rollCorrection = Mathf.Atan(localTarget.x / -localTarget.z) * Mathf.Rad2Deg;
                float targetRoll = Mathf.Clamp(rollCorrection, -RollRange, RollRange);
                float targetPitch = Mathf.Clamp(-localTarget.y, -PitchRange, PitchRange);

                roll = Mathf.Lerp(roll, targetRoll, LerpWeight);
                pitch = Mathf.Lerp(pitch, targetPitch, LerpWeight);
                float targetYaw = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;
                yaw = Mathf.Lerp(yaw, targetYaw, LerpWeight);
            }
            else
            {
                // Once the target is bombed, keep the plane flying straight ahead
                roll = Mathf.Lerp(roll, 0, LerpWeight);
                pitch = Mathf.Lerp(pitch, 0, LerpWeight);
            }
        }
        else
        {
            // Crash the plane
            pitch = Mathf.Lerp(pitch, PitchRange, LerpWeight);
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

    private void HandleBombing()
    {
        bool battleshipInRange = Vector2.Distance(new Vector2(transform.position.x, transform.position.y),
            new Vector2(battleshipRB.transform.position.x, battleshipRB.transform.position.y)) < 20;
        if (!_hasBombDropped && battleshipInRange)
        {
            var bombPos = transform.position - 3 * transform.up;
            var bomb = Instantiate(BombPrefab, bombPos, Quaternion.Euler(100, 0, 0));
            bomb.GetComponent<Rigidbody>().velocity = npcRB.velocity;
            _hasBombDropped = true;
        }
    }

    void FixedUpdate()
    {
        HandleMovement();

        HandleBombing();
    }

    private void TakeHit()
    {
        Debug.Log("Hit!");
        _remainingHp -= DamageTakenPerHit;
        if (_remainingHp <= 0)
        {
            _isCrashing = true;
        }
        // TODO: sounds and effects
    }

    private void OnTriggerEnter(Collider collider)
    {
        // TODO: sounds and effects
        if (collider.GetComponent<BulletBehavior>() != null)
        {
            TakeHit();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // TODO: sounds and effects
        Destroy(gameObject);
    }
}
