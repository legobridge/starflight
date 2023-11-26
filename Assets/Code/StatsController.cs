using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    public GameObject healthbar;
    public int maxHealth;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void UpdateHealthBar()
    {
        healthbar.transform.localScale = new Vector3(
            currentHealth * 1f / maxHealth * 1f,
            healthbar.transform.localScale.y,
            healthbar.transform.localScale.z
            );
    }
}