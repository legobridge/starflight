using UnityEngine;

public class Battleship : MonoBehaviour
{ 
    public float Speed;
    public int MaxHitPoints;
    public int DamageTakenPerHit;

    public GameObject Healthbar;

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
        float healthBarSize = Mathf.Max(0.0f, (float)_remainingHp / (float)MaxHitPoints);
        Healthbar.transform.localScale = new Vector3(
            healthBarSize,
            Healthbar.transform.localScale.y,
            Healthbar.transform.localScale.z
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
