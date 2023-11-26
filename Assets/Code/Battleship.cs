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

    public GameObject healthbar;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            pc.OnGameOver(true);

            Destroy(gameObject);
        }
        // TODO: sounds and effects
    }

}
