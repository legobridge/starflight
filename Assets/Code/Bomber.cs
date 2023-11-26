using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Bomber : MonoBehaviour
{

    //public Transform target; // Assign the target in the inspector

    // Aerodynamic coefficients and movement speeds
    public float ForwardDragCoefficient;
    public float VerticalDragCoefficient;
    public float LiftCoefficient;
    public float PitchRange;
    public float RollRange;
    public float RotationalSpeed;
    public float MaximumThrust;
    public float LerpWeight;

    // HP
    public int MaxHitPoints;
    public int DamageTakenPerHit;

    // Bombing
    public GameObject BombPrefab;
    public int Bombs;

    private Rigidbody npcRB;
    private Rigidbody battleshipRB;
    private float yaw, pitch, roll, thrust;
    private int _remainingHp;
    private bool _isCrashing = false;
    private bool _reachedBattleship = false;
    private int _bombsLeft;
    private float _nextBombDropTime = 0.0f;

    void Start()
    {
        npcRB = GetComponent<Rigidbody>();
        npcRB.velocity = transform.forward * 3;
        battleshipRB = FindObjectOfType<Battleship>().gameObject.GetComponent<Rigidbody>();
        _remainingHp = MaxHitPoints;
        _bombsLeft = Bombs;
    }

    private void HandleMovement()
    {
        if (!_isCrashing)
        {
            Vector3 modifiedTargetPosition = battleshipRB.transform.position;
            modifiedTargetPosition.y += 120;
            modifiedTargetPosition += battleshipRB.transform.forward * battleshipRB.velocity.magnitude * 7;
            Vector3 localTarget = transform.InverseTransformPoint(modifiedTargetPosition);

            if (!_reachedBattleship)
            {
                // If target hasn't been reached yet, fly towards it
                var rollCorrection = Mathf.Atan(localTarget.x / localTarget.z) * Mathf.Rad2Deg * 2;
                float targetRoll = Mathf.Clamp(rollCorrection, -RollRange, RollRange);
                float targetPitch = Mathf.Clamp(-localTarget.y, -PitchRange, PitchRange);

                roll = Mathf.Lerp(roll, targetRoll, LerpWeight);
                pitch = Mathf.Lerp(pitch, targetPitch, LerpWeight);
            }
            else
            {
                // Once the target is reached, keep the plane flying straight ahead
                roll = Mathf.Lerp(roll, 0, LerpWeight);
                pitch = Mathf.Lerp(pitch, 0, LerpWeight);
            }
        }
        else
        {
            // Crash the plane
            pitch = Mathf.Lerp(pitch, PitchRange, LerpWeight);
        }

        yaw -= roll * RotationalSpeed * Time.fixedDeltaTime;
        npcRB.MoveRotation(Quaternion.Euler(pitch, yaw, roll));

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
        if (!_reachedBattleship)
        {
            Vector2 posIn2d = new Vector2(transform.position.x, transform.position.y);
            Vector2 battleshipPosIn2d = new Vector2(battleshipRB.transform.position.x, battleshipRB.transform.position.y);
            float distance2dToBattleship = Vector2.Distance(posIn2d, battleshipPosIn2d);
            _reachedBattleship = distance2dToBattleship < 200;
        }
        else if (Time.time > _nextBombDropTime && _bombsLeft > 0)
        {
            Debug.Log("Bomb");
            var bombPos = transform.position - 3 * transform.up;
            var bomb = Instantiate(BombPrefab, bombPos, Quaternion.Euler(100, 0, 0));
            bomb.GetComponent<Rigidbody>().velocity = npcRB.velocity;
            _bombsLeft -= 1;
            _nextBombDropTime = Time.time + 1.0f;
        }
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    private void Update()
    {
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
