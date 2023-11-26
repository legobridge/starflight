using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //bool bomberHit = other.GetComponent<BomberBehavior>() != null;
        bool bomberHit = false;
        if (bomberHit)
        {
            // TODO: Call bomber's takeDamage method
        }
        // TODO: boom?
        Destroy(gameObject);
    }
}
