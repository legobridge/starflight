using UnityEngine;

public class Battleship : MonoBehaviour
{ 
    public float Speed;
    public int MaxHitPoints;
    public int DamageTakenPerHit;

    public GameObject healthbar;

    public AudioClip BombHitClip;
    public float Volume;

    private Rigidbody rb;
    private AudioSource _audioSource;
    private int _remainingHp;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * Speed;
        _audioSource = GetComponent<AudioSource>();
        _remainingHp = MaxHitPoints;
    }

    public void UpdateHealthBar()
    {
        healthbar.transform.localScale = new Vector3(
            (float) _remainingHp / (float) MaxHitPoints,
            healthbar.transform.localScale.y,
            healthbar.transform.localScale.z
            );
    }

    void TakeDamage()
    {
        _remainingHp -= DamageTakenPerHit;
        UpdateHealthBar();
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
            _audioSource.PlayOneShot(BombHitClip, Volume);
            Destroy(other.gameObject);
        }
    }

}
