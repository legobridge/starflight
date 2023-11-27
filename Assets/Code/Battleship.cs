using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battleship : MonoBehaviour
{ 
    public float Speed;
    public int MaxHitPoints;
    public int DamageTakenPerHit;
    
    private Rigidbody rb;
    private int _remainingHp;
    Vector3 yAxis;

    public GameObject healthbar;

    public AudioSource bombSound;
    public AudioClip clip;
    public static float volume = 0.25f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //Freeze the Y position to prevent the ship from sinking 
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        yAxis = new Vector3(0, 7, 0);

        _remainingHp = MaxHitPoints;
    }

    void FixedUpdate()
    {
        rb.velocity = transform.forward * Speed;
    }

    public void UpdateHealthBar()
    {
        healthbar.transform.localScale = new Vector3(
            _remainingHp * 1f / MaxHitPoints * 1f,
            healthbar.transform.localScale.y,
            healthbar.transform.localScale.z
            );
    }

    void TakeDamage()
    {
        UpdateHealthBar();

        _remainingHp -= DamageTakenPerHit;
        if (_remainingHp <= 0)
        {
            var pc = FindObjectOfType<PlayerControl>();
            pc.OnGameOver(false);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BombBehavior>())
        {
            TakeDamage();
            bombSound.PlayOneShot(clip, volume);
            Destroy(other.gameObject);
        }
    }

}
