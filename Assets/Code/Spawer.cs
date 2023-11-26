using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Prefab;




    /// <summary>
    /// Seconds between spawn operations
    /// </summary>
    public float SpawnInterval = 7;

    /// <summary>
    /// How many units of free space to try to find around the spawned object
    /// </summary>
    public float FreeRadius = 20;

    /// <summary>
    /// Check if we need to spawn and if so, do so.
    /// </summary>
    // ReSharper disable once UnusedMember.Local
    private float spawn = 0.0f;

    void Update()
    {
        var timePassed = Time.timeSinceLevelLoad;
        var zombie = Prefab;
        var pre = Random.Range(0, 2);

        zombie = Prefab;


        if (spawn < timePassed)
        {
            var position = SpawnUtilities.RandomFreePoint(FreeRadius);

            if (position.x <= 0)
            {
                Instantiate(zombie, position, Quaternion.identity);
            }
            else
            {
                Instantiate(zombie, position, Quaternion.Euler(0, 180f, 0));
            }

            spawn += SpawnInterval;

        }
    }

}





