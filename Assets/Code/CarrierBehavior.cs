using UnityEngine;

public class CarrierBehavior : MonoBehaviour
{
    public GameObject BomberPrefab;

    /// <summary>
    /// Seconds between spawn operations
    /// </summary>
    public float SpawnInterval;

    private float _nextSpawnTime = 0;

    void Update()
    {
        if (_nextSpawnTime < Time.timeSinceLevelLoad)
        {
            var position = transform.position + transform.up * 100;
            
            
            var battleship = FindObjectOfType<Battleship>();

            var bomber = Instantiate(BomberPrefab, position, Quaternion.identity);
            bomber.transform.LookAt(battleship.transform);
            _nextSpawnTime += SpawnInterval;

        }
    }

}





