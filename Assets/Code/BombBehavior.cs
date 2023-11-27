using UnityEngine;

public class BombBehavior : MonoBehaviour
{
    public GameObject Explosion1Prefab;
    private void OnTriggerEnter(Collider other)
    {
        GameObject explosion = Instantiate(Explosion1Prefab, transform.position, Quaternion.identity, transform.parent);
        Destroy(explosion, 5f);
        Destroy(gameObject);
    }
}
