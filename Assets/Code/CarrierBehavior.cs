using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrierBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject BomberPrefab;

    /// <summary>
    /// Seconds between spawn operations
    /// </summary>
    public float SpawnInterval;

    /// <summary>
    /// How many units of free space to try to find around the spawned object
    /// </summary>
    public float FreeRadius;

    /// <summary>
    /// Check if we need to spawn and if so, do so.
    /// </summary>
    // ReSharper disable once UnusedMember.Local
    public float NextSpawnTime;
    private Rigidbody npcRB;
    private Rigidbody battleshipRB;

    void Update()
    {
        if (NextSpawnTime < Time.timeSinceLevelLoad)
        {
            var position = transform.position + transform.up * 100;
            
            
            battleshipRB = FindObjectOfType<Battleship>().gameObject.GetComponent<Rigidbody>();

            var bomber = Instantiate(BomberPrefab, position, Quaternion.identity);
            bomber.transform.LookAt(battleshipRB.transform);
            NextSpawnTime += SpawnInterval;

        }
    }

}





