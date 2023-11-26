using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // TODO: sounds and effects
        Destroy(gameObject);
    }
}
