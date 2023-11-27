using UnityEngine;

public class RocketBehavior : MonoBehaviour
{
    public GameObject Explosion1Prefab;

    private void FixedUpdate()
    {
        if (Mathf.Abs(Mathf.Max(transform.position.x, transform.position.y, transform.position.z)) > 2000)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject explosion = Instantiate(Explosion1Prefab, transform.position, Quaternion.identity, transform.parent);
        Destroy(explosion, 5f);
        Destroy(gameObject);
    }
}
